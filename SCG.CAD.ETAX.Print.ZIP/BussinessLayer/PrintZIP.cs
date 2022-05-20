﻿using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.PRINT.ZIP.Controller;
using SCG.CAD.ETAX.XML.PRINT.ZIP.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.PRINT.ZIP.BussinessLayer
{
    public class PrintZIP
    {
        ConfigMftsCompressPrintSettingController configMftsCompressPrintSettingController = new ConfigMftsCompressPrintSettingController();
        OutputSearchPrintingController outputSearchPrintingController = new OutputSearchPrintingController();
        TransactionDescriptionController transactionDescriptionController = new TransactionDescriptionController();
        List<ConfigMftsCompressPrintSetting> configPrintSetting = new List<ConfigMftsCompressPrintSetting>();
        List<TransactionDescription> transactionDescription = new List<TransactionDescription>();

        public void Printzip()
        {
            string zipName = "";
            try
            {
                Console.WriteLine("Start PrintZip");
                GetDataFromDataBase();
                Console.WriteLine("Start Read All PDF");
                var dataallCompany = ReadFile();
                Console.WriteLine("End Read All PDF");
                foreach (var data in dataallCompany)
                {
                    Console.WriteLine("Start Zip Company : " + data.CompanyCode);
                    zipName = data.CompanyCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".7z";
                    var resultZipFile = Zipfile(data, zipName);

                    Console.WriteLine("Insert Data OutputSearchPrinting Company : " + data.CompanyCode + " | ZipName : " + zipName);
                    var resultTransactionPrintZip = InsertTransactionPrintZip(data, zipName);
                    GetListTransactionDescription();
                    if (resultZipFile)
                    {
                        Console.WriteLine("Start Update Status TransactionDescription Company : " + data.CompanyCode);
                        var resultUpdateStatus = UpdateStatusTransactionDescription(data);
                        Console.WriteLine("End Update Status TransactionDescription Company : " + data.CompanyCode);
                    }
                    Console.WriteLine("End Zip Company : " + data.CompanyCode);
                }
                Console.WriteLine("End PrintZip");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FileModel> ReadFile()
        {
            List<FileModel> result = new List<FileModel>();
            string[] fullpath = new string[0];
            string pathFolder = "";
            string fileType = "*.pdf";
            List<string> listpath;
            FileModel fileModel = new FileModel();
            Filedetail Filedetail = new Filedetail();
            string billno = "";
            string filename = "";

            ConfigMftsCompressPrintSetting config = new ConfigMftsCompressPrintSetting();
            //config.ConfigXmlsignInputPath = @"D:\sign";
            //config.ConfigXmlsignOutputPath = @"D:\sign";
            //configXmlSign = new List<ConfigXmlSign>();
            //configXmlSign.Add(config);
            try
            {
                //pathFolder = @"C:\Code_Dev\sign";
                foreach (var path in configPrintSetting)
                {
                    pathFolder = path.ConfigMftsCompressPrintSettingInputPdf;
                    fullpath = Directory.GetFiles(pathFolder, fileType);
                    listpath = fullpath.ToList();

                    fileModel = new FileModel();
                    fileModel.InputPath = path.ConfigMftsCompressPrintSettingInputPdf;
                    fileModel.OutPath = path.ConfigMftsCompressPrintSettingOutputPdf;
                    fileModel.CompanyCode = path.ConfigMftsCompressPrintSettingCompanyCode;
                    fileModel.FileDetails = new List<Filedetail>();
                    foreach (var file in listpath)
                    {
                        Filedetail = new Filedetail();
                        filename = Path.GetFileName(file);
                        Filedetail.FilePath = file;
                        Filedetail.FileName = filename;
                        filename = filename.Replace(".pdf", "");
                        if (filename.IndexOf('_') > -1)
                        {
                            billno = filename.Substring(8, (filename.IndexOf('_')) - 8);
                        }
                        else
                        {
                            billno = filename.Substring(8);
                        }
                        Filedetail.BillingNo = billno;
                        fileModel.FileDetails.Add(Filedetail);
                    }
                    result.Add(fileModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void GetDataFromDataBase()
        {
            try
            {
                configPrintSetting = configMftsCompressPrintSettingController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetListTransactionDescription()
        {
            try
            {
                transactionDescription = transactionDescriptionController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Zipfile(FileModel dataFile, string zipName)
        {
            bool result = false;
            string zipPath = "";
            try
            {
                //zipPath = @"D:\Example\result.zip";
                zipPath = dataFile.OutPath;
                if (!Directory.Exists(zipPath))
                {
                    Directory.CreateDirectory(zipPath);
                }

                using (FileStream zipFileToOpen = new FileStream(zipPath + "\\" + zipName, FileMode.OpenOrCreate))
                {
                    using (ZipArchive archive = new ZipArchive(zipFileToOpen, ZipArchiveMode.Create))
                    {
                        foreach (var file in dataFile.FileDetails)
                        {
                            Console.WriteLine("Zip Company : " + dataFile.CompanyCode + " | File Name : " + file.FileName);
                            archive.CreateEntryFromFile(file.FilePath, file.FileName);
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool InsertTransactionPrintZip(FileModel dataFile, string zipName)
        {
            bool result = false;
            try
            {
                Task<Response> res;
                OutputSearchPrinting insertData = new OutputSearchPrinting();
                insertData.OutputSearchPrintingCompanyCode = dataFile.CompanyCode;
                insertData.OutputSearchPrintingFileName = zipName;
                insertData.OutputSearchPrintingFullPath = dataFile.OutPath + "\\" + zipName;
                insertData.CreateBy = "Batch";
                insertData.CreateDate = DateTime.Now;
                insertData.UpdateBy = "Batch";
                insertData.UpdateDate = DateTime.Now;
                insertData.Isactive = 1;
                
                var json = JsonSerializer.Serialize(insertData);
                res = outputSearchPrintingController.Insert(json);
                if (res.Result.MESSAGE == "Insert success.")
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool UpdateStatusTransactionDescription(FileModel dataFile)
        {
            bool result = false;
            try
            {
                Task<Response> res;
                TransactionDescription updatetransaction = new TransactionDescription();
                List<TransactionDescription> listupdatetransaction = new List<TransactionDescription>();
                foreach(var filedata in dataFile.FileDetails)
                {
                    updatetransaction = transactionDescription.FirstOrDefault(x => x.BillingNumber == filedata.BillingNo);
                    if(updatetransaction != null)
                    {
                        Console.WriteLine("Update Status TransactionDescription BillingNo : " + filedata.BillingNo);
                        updatetransaction.PrintStatus = "Successful";
                        updatetransaction.PrintDetail = "PDF file's was prepared for printing completely";
                        updatetransaction.PrintDateTime = DateTime.Now;
                        listupdatetransaction.Add(updatetransaction);
                    }
                }
                if(listupdatetransaction.Count > 0)
                {
                    var json = JsonSerializer.Serialize(listupdatetransaction);
                    res = transactionDescriptionController.UpdateList(json);
                    if (res.Result.MESSAGE == "Updated Success.")
                    {
                        result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}