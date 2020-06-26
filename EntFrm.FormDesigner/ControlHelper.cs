using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Reflection;

namespace MyFormDesinger
{
    class ControlHelper
    {
        public static Control CreateControl(string ctrlName, string path)
        {
            try
            {
                Control ctrl = null;
                switch (ctrlName)
                {
                    case "Label":
                        ctrl = new Label();
                        break;
                    case "TextBox":
                        ctrl = new TextBox();
                        break;
                    case "PictureBox":
                        ctrl = new PictureBox();
                        break;
                    case "ListView":
                        ctrl = new ListView();
                        break;
                    case "ComboBox":
                        ctrl = new ComboBox();
                        break;
                    case "Button":
                        ctrl = new Button();
                        break;
                    case "CheckBox":
                        ctrl = new CheckBox();
                        break;
                    case "MonthCalender":
                        ctrl = new MonthCalendar();
                        break;
                    case "DateTimePicker":
                        ctrl = new DateTimePicker();
                        break;
                    case "RadioButton":
                        ctrl = new RadioButton();
                        break;
                    case "ProgressBar":
                        ctrl = new ProgressBar();
                        break;
                    case "TreeView":
                        ctrl = new TreeView();
                        break;
                    case "LinkLabel":
                        ctrl = new LinkLabel();
                        break;
                    case "ListBox":
                        ctrl = new ListBox();
                        break;
                    default: //其他
                        string[] strs = path.Split('/');
                        if (strs.Length == 2)
                        {
                            Assembly controlAsm = Assembly.LoadFile(strs[1]);
                            Type controlType = controlAsm.GetType(strs[0]);
                            ctrl = (Control)Activator.CreateInstance(controlType);
                        }
                        break;

                }
                return ctrl;

            }
            catch (Exception ex) //创建失败
            {
                return new Control();
            }	
        }
    }
}
