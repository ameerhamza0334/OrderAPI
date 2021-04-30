using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Orders.Service.ViewModels;
using OrdersAPI;
using OrdersAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Path = System.IO.Path;

namespace Orders.Service.Helpers
{
	public class PdfHelper
	{
		private PdfDocument PdfDocument;
		private Document Document;
		private static string FileName;
		private string FullPath;

		public PdfHelper()
		{
			FileName = $"{Guid.NewGuid().ToString()}.pdf";
			string TempPath = Path.Combine(Path.GetTempPath(), "TempPDFs");
			Directory.CreateDirectory(TempPath);
			FullPath = Path.Combine(TempPath, FileName);
			PdfDocument = new PdfDocument(
				new PdfWriter(
				new FileStream(FullPath
				, FileMode.Create)));
			Document = new Document(PdfDocument, PageSize.B7);
		}

		private IBlockElement AddElementtoDoc(string Text, TextAlignment textAlignment, int size, params float[] margins)
		{
			Paragraph element;
			if (margins.Length > 0)
			{
				element = new Paragraph(Text)
					.SetTextAlignment(textAlignment)
					.SetFontSize(size)
					.SetMargins(margins[0], margins[1], margins[2], margins[3]);
			}
			else
			{
				element = new Paragraph(Text)
				.SetTextAlignment(textAlignment)
				.SetFontSize(size);
			}
			Document.Add(element);
			return element;
		}

		private Cell CreateCell(string text, TextAlignment textAlignment = TextAlignment.CENTER, int rowspan = 1, int colspan = 1)
		{
			return new Cell(rowspan, colspan)
				.SetBorder(Border.NO_BORDER)
				.SetTextAlignment(textAlignment)
				.SetFontSize(10)
				.Add(new Paragraph(text));
		}

		private IBlockElement AddTable(List<OrdersDetailRequestModel> models)
		{
			Table table = new Table(3, false)
					.SetBorder(Border.NO_BORDER).SetWidth(180f);
			table.AddCell(CreateCell("Name", TextAlignment.LEFT).SetWidth(50f));
			table.AddCell(CreateCell("QTY", TextAlignment.RIGHT).SetWidth(25f));
			table.AddCell(CreateCell("Price", TextAlignment.RIGHT).SetWidth(25f));
			models.ForEach(val =>
			{
				table.AddCell(CreateCell(val.Name, TextAlignment.LEFT).SetWidth(50f));
				table.AddCell(CreateCell(val.Quantity.ToString(), TextAlignment.RIGHT).SetWidth(25f));
				table.AddCell(CreateCell(val.Price.ToString(), TextAlignment.RIGHT).SetWidth(25f));
			});

			Document.Add(table);
			return table;
		}

		public FileResponseModel CreateFileFromModel(OrdersRequestModel ordersRequestModel)
		{
			AddElementtoDoc(ordersRequestModel.CompanyName, TextAlignment.CENTER, 12);
			AddElementtoDoc(ordersRequestModel.Address, TextAlignment.CENTER, 10);
			AddElementtoDoc($"Phone: {ordersRequestModel.PhoneNumber}", TextAlignment.CENTER, 10);
			AddElementtoDoc($"-----------------------------------------------------", TextAlignment.CENTER, 10);
			AddTable(ordersRequestModel.ordersDetails);
			AddElementtoDoc($"-----------------------------------------------------", TextAlignment.CENTER, 10);
			AddElementtoDoc($"Discount:{ordersRequestModel.OverallDiscount}", TextAlignment.RIGHT, 10, 0, 0, 0, 0);
			AddElementtoDoc($"Tax:{ordersRequestModel.Tax}", TextAlignment.RIGHT, 10, 0, 0, 0, 0);
			AddElementtoDoc($"Total:{ordersRequestModel.ordersDetails.Select(x => x.Price).Sum()}", TextAlignment.RIGHT, 10, 0, 0, 0, 0);
			PdfDocument.Close();
			Document.Close();

			return new FileResponseModel(File.ReadAllBytes(FullPath), FullPath);
		}
	}
}
