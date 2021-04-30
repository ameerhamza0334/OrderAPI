using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Service.ViewModels
{
	public class FileResponseModel
	{
		public FileResponseModel(byte[] _file, string _filename)
		{
			File = _file;
			FileName = _filename;
		}
		public byte[] File { get; set; }
		public string FileName { get; set; }
	}
}
