using System;

namespace EntFrm.Business.Model
{
  public class ContentClass
  {
     public ContentClass(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string ClassNo;
       public string sClassNo
       {
           set { this.ClassNo =value;}
           get { return this.ClassNo;}
        }

       private string ClassName;
       public string sClassName
       {
           set { this.ClassName =value;}
           get { return this.ClassName;}
        }

       private string ParentNo;
       public string sParentNo
       {
           set { this.ParentNo =value;}
           get { return this.ParentNo;}
        }

       private int OrderNo;
       public int iOrderNo
       {
           set { this.OrderNo =value;}
           get { return this.OrderNo;}
        }

       private string BranchNo;
       public string sBranchNo
       {
           set { this.BranchNo =value;}
           get { return this.BranchNo;}
        }

       private string AddOptor;
       public string sAddOptor
       {
           set { this.AddOptor =value;}
           get { return this.AddOptor;}
        }

       private DateTime AddDate;
       public DateTime dAddDate
       {
           set { this.AddDate =value;}
           get { return this.AddDate;}
        }

       private string ModOptor;
       public string sModOptor
       {
           set { this.ModOptor =value;}
           get { return this.ModOptor;}
        }

       private DateTime ModDate;
       public DateTime dModDate
       {
           set { this.ModDate =value;}
           get { return this.ModDate;}
        }

       private int ValidityState;
       public int iValidityState
       {
           set { this.ValidityState =value;}
           get { return this.ValidityState;}
        }

       private string Comments;
       public string sComments
       {
           set { this.Comments =value;}
           get { return this.Comments;}
        }

       private string AppCode;
       public string sAppCode
       {
           set { this.AppCode =value;}
           get { return this.AppCode;}
        }

       private string Version;
       public string sVersion
       {
           set { this.Version =value;}
           get { return this.Version;}
        }

    }
  }

