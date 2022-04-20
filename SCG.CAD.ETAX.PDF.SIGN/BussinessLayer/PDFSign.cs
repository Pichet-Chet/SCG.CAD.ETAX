﻿using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCG.CAD.ETAX.PDF.SIGN.Controller;
using SCG.CAD.ETAX.MODEL.etaxModel;

namespace SCG.CAD.ETAX.PDF.SIGN.BussinessLayer
{
    public class PDFSign
    {
        public string[] ReadPdfFile()
        {
            string[] result = new string[0];
            try
            {
                StringBuilder sb = new StringBuilder();
                string pathFolder = @"D:\sign";
                string fileType = "*.pdf";
                result = Directory.GetFiles(pathFolder, fileType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void PdfSign()
        {
            string folderDest = @"D:/";
            string fileNameDest = "";
            string fileType = ".pdf";
            bool resultPDFSign = false;
            try
            {
                var allfile = ReadPdfFile();
                if (allfile != null && allfile.Length > 0)
                {
                    var alldataTransactionDes = CheckDatainDataBase();
                    foreach (string src in allfile)
                    {
                        fileNameDest = Path.GetFileName(src).Replace(".pdf", "");
                        PdfReader reader = new PdfReader(src);
                        FileStream os = new FileStream(folderDest + fileNameDest + "_sign" + fileType, FileMode.Create);
                        PdfStamper stamper = PdfStamper.CreateSignature(reader, os, '\0');
                        resultPDFSign = SendFilePDFSign();
                        if (alldataTransactionDes.Count > 0)
                        {
                            UpdateStatusAfterSignPDF(resultPDFSign);
                        }
                        else
                        {
                            InsertDataAfterSignPDF(resultPDFSign);
                        }

                        ExportPDFAfterSign(resultPDFSign);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SendFilePDFSign()
        {
            bool result = false;
            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        
        public bool UpdateStatusAfterSignPDF(bool status)
        {
            bool result = false;
            string jsondata = "";
            try
            {

                TransactionDescriptionController transactiondescriptioncontroller = new TransactionDescriptionController();
                if (status)
                {

                }
                else
                {

                }
                var jsonresult = transactiondescriptioncontroller.Update(jsondata);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool InsertDataAfterSignPDF(bool status)
        {
            bool result = false;
            string jsondata = "";
            try
            {

                TransactionDescriptionController transactiondescriptioncontroller = new TransactionDescriptionController();
                if (status)
                {

                }
                else
                {

                }
                var jsonresult = transactiondescriptioncontroller.Insert(jsondata);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<TransactionDescription> CheckDatainDataBase()
        {
            List<TransactionDescription> result = new List<TransactionDescription>();
            try
            {
                TransactionDescriptionController transactiondescriptioncontroller = new TransactionDescriptionController();
                result = transactiondescriptioncontroller.List().Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void ExportPDFAfterSign(bool resultPDFSign)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
