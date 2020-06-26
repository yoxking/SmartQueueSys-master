using EntFrm.Framework.Utility;
using System.Collections.Generic;

namespace MyFormDesinger
{
    public class IPublicEntity
    {
        public const string TYPE_BACKGROUND = "Background";
        public const string DEF_BGIMAGE = "BgImage";


        public static List<ItemObject> GetShowDialogs()
        {
            List<ItemObject> dialogs = new List<ItemObject>();

            dialogs.Add(new ItemObject("无", "Null"));
            dialogs.Add(new ItemObject("刷身份证（姓名）", "ScanCardDialog1"));
            dialogs.Add(new ItemObject("刷身份证（票号）", "ScanCardDialog2")); 
            dialogs.Add(new ItemObject("刷身份证+手动输入(姓名)", "MyIdCardDialog1"));
            dialogs.Add(new ItemObject("刷身份证+手动输入(票号)", "MyIdCardDialog2"));
            dialogs.Add(new ItemObject("刷身份证+车牌输入(姓名)", "PlateInputDialog1"));
            dialogs.Add(new ItemObject("刷身份证+车牌输入(票号)", "PlateInputDialog2"));
            dialogs.Add(new ItemObject("刷社保卡(票号)", "ScanDecardDialog"));//德卡t6
            dialogs.Add(new ItemObject("扫条形码(票号)", "ScanBarcodeDialog"));
            dialogs.Add(new ItemObject("手写输入(票号)", "MyHandInputDialog"));
            //dialogs.Add(new ItemObject("全数字输入", "NumBoardDialog")); 

            return dialogs;
        }
    }
}
