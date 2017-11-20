﻿using System;
using System.IO;
using System.Configuration;
using CapturesQueue.Helpers;


namespace CapturesQueue.Services
{
    public class CasperJsService
    {
        private readonly CasperJsHelper.CasperJsHelper _cjs;
        private readonly string _pathToJsFile;
        private readonly string _screenShotFolder;

        public CasperJsService()
        {
            _cjs = new CasperJsHelper.CasperJsHelper(@"..\..\casperjs-1.1.3");
            _pathToJsFile = ConfigurationManager.AppSettings["Casper.PathToJS"];
            _screenShotFolder = ConfigurationManager.AppSettings["Casper.ScreenShotFolder"];
        }

        public  byte[] GetScreenBytesByUrl(string url)
        {
            var scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _pathToJsFile);
            var tempFileName = Guid.NewGuid() + ".png";
            _cjs.Run(scriptPath.AddCommasIfRequired(), new string[] { $"--url={url}", $"--fileName={tempFileName}" });

            var screenPath = Path.Combine(_cjs.ToolPath, _screenShotFolder, tempFileName);
            var result = File.ReadAllBytes(screenPath);
            File.Delete(screenPath);
            return result;
        }
    }
}