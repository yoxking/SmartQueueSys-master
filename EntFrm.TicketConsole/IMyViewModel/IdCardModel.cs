using System;

namespace EntFrm.TicketConsole
{

    public class IdCardModel
    {
        private string CnName;

        public string sCnName
        {
            get { return CnName; }
            set { CnName = value; }
        }
        private string Sex;

        public string sSex
        {
            get { return Sex; }
            set { Sex = value; }
        }
        private string Nation;

        public string sNation
        {
            get { return Nation; }
            set { Nation = value; }
        }
        private DateTime BirthDate;

        public DateTime dBirthDate
        {
            get { return BirthDate; }
            set { BirthDate = value; }
        }
        private string BirthPlace;

        public string sBirthPlace
        {
            get { return BirthPlace; }
            set { BirthPlace = value; }
        }
        private string IdCardNo;

        public string sIdCardNo
        {
            get { return IdCardNo; }
            set { IdCardNo = value; }
        }
        private string Picture;

        public string sPicture
        {
            get { return Picture; }
            set { Picture = value; }
        } 

        

        public IdCardModel(string IdCardStr)
        {
            if (IdCardStr.Length > 0)
            {
                this.CnName = IdCardStr.Substring(0, 15).Trim(); 

                this.Sex = ConvertSex(int.Parse(IdCardStr.Substring(15, 1)));
                this.Nation = ConvertNation(int.Parse(IdCardStr.Substring(16, 2)));
                string year = IdCardStr.Substring(18, 4);
                string month = IdCardStr.Substring(22, 2);
                string day = IdCardStr.Substring(24, 2);
                this.BirthDate = DateTime.Parse(year + "-" + month + "-" + day);
                this.BirthPlace = IdCardStr.Substring(26, 35).Trim();
                this.IdCardNo = IdCardStr.Substring(61, 18); 
            } 
        } 

        private string ConvertNation(int NationNo)
        {
            string sResult = "汉";

            switch (NationNo)
            {
                case 01: sResult = "汉"; break;
                case 02: sResult = "蒙古"; break;
                case 03: sResult = "回"; break;
                case 04: sResult = "藏"; break;
                case 05: sResult = "维吾尔"; break;
                case 06: sResult = "苗"; break;
                case 07: sResult = "彝"; break;
                case 08: sResult = "壮"; break;
                case 09: sResult = "布依"; break;
                case 10: sResult = "朝鲜"; break;
                case 11: sResult = "满"; break;
                case 12: sResult = "侗"; break;
                case 13: sResult = "瑶"; break;
                case 14: sResult = "白"; break;
                case 15: sResult = "土家"; break;
                case 16: sResult = "哈尼"; break;
                case 17: sResult = "哈萨克"; break;
                case 18: sResult = "傣"; break;
                case 19: sResult = "黎"; break;
                case 20: sResult = "傈僳"; break;
                case 21: sResult = "佤"; break;
                case 22: sResult = "畲"; break;
                case 23: sResult = "高山"; break;
                case 24: sResult = "拉祜"; break;
                case 25: sResult = "水"; break;
                case 26: sResult = "东乡"; break;
                case 27: sResult = "纳西"; break;
                case 28: sResult = "景颇"; break;
                case 29: sResult = "柯尔克孜"; break;
                case 30: sResult = "土"; break;
                case 31: sResult = "达斡尔"; break;
                case 32: sResult = "仫佬"; break;
                case 33: sResult = "羌"; break;
                case 34: sResult = "布朗"; break;
                case 35: sResult = "撒拉"; break;
                case 36: sResult = "毛南"; break;
                case 37: sResult = "仡佬"; break;
                case 38: sResult = "锡伯"; break;
                case 39: sResult = "阿昌"; break;
                case 40: sResult = "普米"; break;
                case 41: sResult = "塔吉克"; break;
                case 42: sResult = "怒"; break;
                case 43: sResult = "乌孜别克"; break;
                case 44: sResult = "俄罗斯"; break;
                case 45: sResult = "鄂温克"; break;
                case 46: sResult = "德昂"; break;
                case 47: sResult = "保安"; break;
                case 48: sResult = "裕固"; break;
                case 49: sResult = "京"; break;
                case 50: sResult = "塔塔尔"; break;
                case 51: sResult = "独龙"; break;
                case 52: sResult = "鄂伦春"; break;
                case 53: sResult = "赫哲"; break;
                case 54: sResult = "门巴"; break;
                case 55: sResult = "珞巴"; break;
                case 56: sResult = "基诺"; break;
                default: sResult = "未定民"; break;
            }
            return sResult + "族";
        }

        private string ConvertSex(int SexNo)
        {
            string sResult = "男";

            switch (SexNo)
            {
                case 1: sResult = "男"; break;
                case 2: sResult = "女"; break;
                case 9: sResult = "其他"; break;
                default: break;
            }

            return sResult;
        }
    }
}
