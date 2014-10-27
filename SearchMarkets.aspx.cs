using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Web.UI.HtmlControls;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    public void searchbyplace(object sender, EventArgs e)
    {
       

         String URL = "https://maps.googleapis.com/maps/api/place/textsearch/xml?query=";
         String key = "&key=AIzaSyCIDxyi3jR-1Reb8xaqLKzXBD5-UQ6Ys64";

        String res = SearchMarkets(market.Value, URL,key);

       tbl.Controls.Clear();

/*
       for (int i = 0; i < 10; i++)
       {
           TableRow rowNew = new TableRow();
           tbl.Controls.Add(rowNew);
           for (int j = 0; j < 3; j++)
           {
               TableCell cellNew = new TableCell();
               cellNew.Controls.Add(lblNew);
               cellNew.Controls.Add(imgNew);
               rowNew.Controls.Add(cellNew);
           }
       }*/
       String xmlUrl = "E:\\softwares\\EMercado\\App_Data\\a.xml";
     //  XmlTextReader reader = new XmlTextReader(xmlUrl);
       /* while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element: // The node is an element.
                    Response.Write("<" + reader.Name + ">");
                    break;
                case XmlNodeType.Text: //Display the text in each element.
                    Response.Write(reader.Value + "<br />");
                    break;
                case XmlNodeType.EndElement: //Display the end of the element.
                    Response.Write("</" + reader.Name + ">");
                    break;
            }
        }
        reader.Close();*/
        //

        //bharat
      

        XmlDocument doc = new XmlDocument();
        doc.Load(xmlUrl);

        XmlNode PlaceSearchNode =
                doc.SelectSingleNode("/PlaceSearchResponse");

        XmlNodeList MarketNodeList =
                PlaceSearchNode.SelectNodes("result");
        HtmlTableRow row1 = new HtmlTableRow();

        HtmlTableCell headerCell = new HtmlTableCell();
        HtmlTableCell headerCell1 = new HtmlTableCell();

        headerCell.InnerHtml = "<b> Market Name </b>";
        headerCell1.InnerHtml = "<b> Address </b>";

        row1.Cells.Add(headerCell);
        row1.Cells.Add(headerCell1);
        row1.BorderColor = "blue";

       
        tbl.Rows.Add(row1);




        foreach (XmlNode node in MarketNodeList)
        {

                HtmlTableRow row = new HtmlTableRow();
                HtmlTableCell nameCell = new HtmlTableCell();
                HtmlTableCell addCell = new HtmlTableCell();


                String s;

                s = node.SelectSingleNode("name").InnerText;
                nameCell.InnerText = s;

                s = node.SelectSingleNode("formatted_address").InnerText;
                addCell.InnerText = s;

                row.Cells.Add(nameCell);
                row.Cells.Add(addCell);
                row.BorderColor = "blue";

                tbl.Rows.Add(row);

        }
        File.Delete(xmlUrl);


    }

     

    public String SearchMarkets(String queryString, String Url, String Key)
    {
        String targetUrl = Url+queryString+Key;

        WebRequest request = WebRequest.Create(targetUrl);
        request.Method = "GET";
        WebResponse response = request.GetResponse();
        StreamReader sr = new StreamReader(response.GetResponseStream());
        String resp = sr.ReadToEnd();
        String path = "E:\\softwares\\EMercado\\App_Data\\a.xml";
        StreamWriter sw = File.CreateText(path);
        sw.Write(resp);
        sw.Close();
        	
        return resp;
    }
 

     
}