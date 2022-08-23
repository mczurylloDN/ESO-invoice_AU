
using ESO_InvoicesDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

//Script demo to fix ESO Invoices
//DN Szczecin ESO L2 
//Last Update / Creation Date 08.01.2022 
//Author: Jose Frutos Solution Expert with just a notch of Michał Czuryłło's code
//****************************************************************************************
//Begin script 
//Variables and SetUp of enviroment

Console.WriteLine("XML Doc Parser for ESO INVOICES");
Console.WriteLine("please choose which market are you want to check \n"+
    "write \"AU\" for australia \n"+
    "or \"US\" for USA \n");
string market = Console.ReadLine();

string ROOT_PATH = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString();
string strWorkPath = System.IO.Path.GetDirectoryName(ROOT_PATH).ToString();
string FullInputPath = strWorkPath + "\\Input";

//After we set up the full path location we extract all xml files and place them in an array
//then the recursive function will fw each file to the helper class to be manipulated.
string[] Filtered = Directory.GetFiles(FullInputPath, "*.xml"); // xml files all
List<ESOInvoiceSuperObject> Results = new List<ESOInvoiceSuperObject>();
//Redirect to process class 
foreach (var item in Filtered)
{
   //We place the whole Invoices found on the folder in the List Of Superobject
   Results.Add(HelperClass.ReviewFile(item));
}

//After we got all the objects build we can check for the issues
if (market == "AU" | market == "au") 
{ 
    List<ResultsObject> FilesCheckedResults = new List<ResultsObject>();
    //Check for Item name Long issue
    FilesCheckedResults = HelperClass.CheckInvoicesforItemLongName(Results).Distinct().ToList();
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("Checking files for item Name longer than 50 Chars");
    Console.WriteLine("_________________________________________________________________________________________________");
    foreach (var item in FilesCheckedResults)
    {
        Console.WriteLine("Invoice number  -> " + item.InvoiceNumber_p + "   " + item.Errors_p);
    }
    FilesCheckedResults.Clear();
    Console.WriteLine();
    //Here we check for duplicates 
    FilesCheckedResults = HelperClass.FindDuplicateItem(Results);
    Console.WriteLine();
    Console.WriteLine("Checking for duplicated Product Code ");
    Console.WriteLine("_________________________________________________________________________________________________");
    foreach (var item in FilesCheckedResults)
    {
        Console.WriteLine( "INVOICE NUMBER  -> " + item.InvoiceNumber_p + "   " + item.Errors_p);
    }
    Console.ReadKey();
}
else if (market == "US" | market == "us")
{ 
    List<ResultsObject> FilesCheckedResults = new List<ResultsObject>();
    //Check for Item name Long issue
    FilesCheckedResults = HelperClass.CheckInvoicesforItemLongName(Results).Distinct().ToList();
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("Checked files for Item Name longer than 50 Chars");
    Console.WriteLine("_________________________________________________________________________________________________");
foreach (var item in FilesCheckedResults)
{
    Console.WriteLine("INVOICE NUMBER  -> " +  item.InvoiceNumber_p + "   " + item.Errors_p);

}
    FilesCheckedResults.Clear();
    Console.WriteLine();
    //Here we check for duplicates 
    FilesCheckedResults = HelperClass.FindDuplicateItem(Results);
    Console.WriteLine();
    Console.WriteLine("Duplicated Product Code ");
    Console.WriteLine("_________________________________________________________________________________________________");
foreach (var item in FilesCheckedResults)
{
    Console.WriteLine( "INVOICE NUMBER  -> " + item.InvoiceNumber_p + "   " + item.Errors_p);
}
    Console.ReadKey();
}
else
{   
    Console.WriteLine("please enter correct market");
}
