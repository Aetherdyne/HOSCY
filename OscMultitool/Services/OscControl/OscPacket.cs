﻿using System;
using System.Collections.Generic;

namespace OscMultitool.OscControl
{
    public readonly struct OscPacket
    {
        public readonly string Address { get; init; } = string.Empty;
        public readonly string Ip { get; init; } = string.Empty;
        public readonly int Port { get; init; } = -1;
        public readonly object[] Variables { get; init; } = Array.Empty<object>();
        public bool IsValid => ValidatePacket();

        public OscPacket(string address, string ip, int port, params object[] variables)
        {
            Address = address;
            Ip = ip;
            Port = port;
            Variables = variables;
        }

        public OscPacket(string address, params object[] variables)
        {
            Address = address;
            Variables = variables;
            Ip = Config.Osc.Ip;
            Port = Config.Osc.Port;
        }

        /// <summary>
        /// Basic validity check for packets (Checking basic ip validity, if packet is empty and if port is valid)
        /// </summary>
        /// <returns>Valid?</returns>
        private bool ValidatePacket()
        {
            if (string.IsNullOrWhiteSpace(Address) || string.IsNullOrWhiteSpace(Ip) || Variables.Length == 0)
                return false;

            if (Port < 1 || Port > 65535)
                return false;

            return true;
        }

        /// <summary>
        /// Override to convert to string for logging purposes
        /// </summary>
        /// <returns>String containing IP, Port, Address and Arguments</returns>
        public override string ToString()
        {
            var argsInfo = new List<string>();
            foreach (var arg in Variables)
                argsInfo.Add($"{arg.GetType().Name}({arg})" ?? "???");

            return $"{Ip}:{Port} ({Address}) => {string.Join(", ", argsInfo)}";
        }

    }
}
