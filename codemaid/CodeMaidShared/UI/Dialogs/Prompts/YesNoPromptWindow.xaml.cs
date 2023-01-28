﻿using System.Reflection;
using System.Windows;

namespace SteveCadwallader.CodeMaid.UI.Dialogs.Prompts
{
    /// <summary>
    /// Interaction logic for YesNoPromptWindow.xaml
    /// </summary>
    public partial class YesNoPromptWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YesNoPromptWindow" /> class.
        /// </summary>
        public YesNoPromptWindow()
        {
            Application.ResourceAssembly = Assembly.GetExecutingAssembly();

            InitializeComponent();
        }
    }
}