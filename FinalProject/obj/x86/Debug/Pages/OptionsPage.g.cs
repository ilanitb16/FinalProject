﻿#pragma checksum "C:\Users\ilani\Documents\School\2022\FinalProject\FinalProject\Pages\OptionsPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "09D4AAD56CED8BCE58381AE2349B893E6013892ECAF8DD35477E4A0E4FBFDDA2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalProject.Pages
{
    partial class OptionsPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Pages\OptionsPage.xaml line 116
                {
                    global::Windows.UI.Xaml.Documents.Hyperlink element2 = (global::Windows.UI.Xaml.Documents.Hyperlink)(target);
                    ((global::Windows.UI.Xaml.Documents.Hyperlink)element2).Click += this.HomeButton_Click;
                }
                break;
            case 3: // Pages\OptionsPage.xaml line 119
                {
                    global::Windows.UI.Xaml.Documents.Hyperlink element3 = (global::Windows.UI.Xaml.Documents.Hyperlink)(target);
                    ((global::Windows.UI.Xaml.Documents.Hyperlink)element3).Click += this.SettingsButton_Click;
                }
                break;
            case 4: // Pages\OptionsPage.xaml line 122
                {
                    global::Windows.UI.Xaml.Documents.Hyperlink element4 = (global::Windows.UI.Xaml.Documents.Hyperlink)(target);
                    ((global::Windows.UI.Xaml.Documents.Hyperlink)element4).Click += this.StoreButton_Click;
                }
                break;
            case 5: // Pages\OptionsPage.xaml line 125
                {
                    global::Windows.UI.Xaml.Documents.Hyperlink element5 = (global::Windows.UI.Xaml.Documents.Hyperlink)(target);
                    ((global::Windows.UI.Xaml.Documents.Hyperlink)element5).Click += this.HelpButton_Click;
                }
                break;
            case 6: // Pages\OptionsPage.xaml line 128
                {
                    global::Windows.UI.Xaml.Documents.Hyperlink element6 = (global::Windows.UI.Xaml.Documents.Hyperlink)(target);
                    ((global::Windows.UI.Xaml.Documents.Hyperlink)element6).Click += this.LoginButton_Click;
                }
                break;
            case 7: // Pages\OptionsPage.xaml line 86
                {
                    this.musicButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.musicButton).Click += this.musicButton_Click;
                }
                break;
            case 8: // Pages\OptionsPage.xaml line 58
                {
                    this.soundButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.soundButton).Click += this.soundButton_Click;
                }
                break;
            case 9: // Pages\OptionsPage.xaml line 31
                {
                    this.PlayButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.PlayButton).Click += this.PlayButton_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
