﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OscMultitool.Ui.Windows
{
    /// <summary>
    /// Interaction logic for ModifyDictionaryWindow.xaml
    /// </summary>
    public partial class ModifyDictionaryWindow : Window
    {
        private readonly Dictionary<string, string> _dict;

        public ModifyDictionaryWindow(string title, Dictionary<string, string> dict)
        {
            InitializeComponent();

            _dict = dict;
            Title = title;
            Refresh();
        }

        private void Refresh()
            => UiHelper.RefreshListBox(listBox, _dict.Select(x => $"{x.Key} : {x.Value}"));

        private void Button_AddOrModifyEntry(object sender, RoutedEventArgs e)
            => AddOrModify();

        private void TextBox_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                AddOrModify();
        }

        private void AddOrModify()
        {
            var key = GetTextValue(textKey.Text);
            var newIndex = listBox.SelectedIndex;

            if (_dict.ContainsKey(key))
            {
                _dict[key] = GetTextValue(textValue.Text);
            }
            else
            {
                _dict.Add(key, GetTextValue(textValue.Text));
                newIndex = listBox.SelectedIndex = _dict.Count - 1;
            }

            Refresh();
            listBox.SelectedIndex = newIndex;
        }

        private void Button_RemoveEntry(object sender, RoutedEventArgs e)
        {
            if (_dict.Count == 0 || listBox.SelectedIndex == -1)
                return;

            int index = listBox.SelectedIndex;
            _dict.Remove(_dict.Keys.ToArray()[index]);
            Refresh();
            listBox.SelectedIndex = index - 1;
        }

        private static string GetTextValue(string text)
        {
            text = text.Trim();
            return string.IsNullOrWhiteSpace(text) ? "New Value" : text;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex > _dict.Count - 1)
                listBox.SelectedIndex = _dict.Count - 1;

            if (listBox.SelectedIndex < 0)
                return;

            int index = listBox.SelectedIndex;
            textKey.Text = _dict.Keys.ToArray()[index];
            textValue.Text = _dict[textKey.Text];
        }
    }
}
