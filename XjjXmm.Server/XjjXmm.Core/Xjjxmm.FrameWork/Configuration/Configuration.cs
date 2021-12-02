using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.Configuration;
using XjjXmm.FrameWork.LogExtension;

namespace XjjXmm.FrameWork.Configuration
{
    public class XjjXmmConfiguration
    {
        private static ILog<DefaultLogger> Logger => App.Logger;
        private static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="environmentName">环境名称</param>
        /// <param name="reloadOnChange">自动更新</param>
        /// <returns></returns>
        internal void Scan(string environmentName = "", bool reloadOnChange = false)
        {
            //var filePath = Path.Combine(AppContext.BaseDirectory, "configs");
            //if (!Directory.Exists(filePath))
            //    return;

            var filePath = AppContext.BaseDirectory;

            var config = new ConfigurationBuilder()
                .SetBasePath(filePath);

            var configFilePath = Path.Combine(AppContext.BaseDirectory, "configs");
            if (Directory.Exists(configFilePath))
            {


                //var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                //var environment = config["Environment"];
                foreach (var enumerateFile in Directory.EnumerateFiles(configFilePath, "*.json"))
                {
                    //if(enumerateFile.ToLower().Contains(".development.") && environment?.ToLower() != "development")
                    //{
                    //      continue;
                    //}
                    //else if (!enumerateFile.ToLower().Contains(".development.") && environment?.ToLower() == "development")
                    //{
                    //    continue;
                    //}
                    //else  if (!enumerateFile.ToLower().EndsWith(".json"))
                    //{
                    //    continue;
                    //}

                    var file = new FileInfo(enumerateFile);
                    //if (!file.Name.ToLower().EndsWith(".json"))
                    //{
                    //     continue;
                    //}

                    var files = file.Name.Split(".");
                    if (files.Length == 2)
                    {
                        config.AddJsonFile("configs/" + file.Name, true, reloadOnChange);
                    }
                }


                foreach (var enumerateFile in Directory.EnumerateFiles(configFilePath, "*.json"))
                {
                    //if(enumerateFile.ToLower().Contains(".development.") && environment?.ToLower() != "development")
                    //{
                    //      continue;
                    //}
                    //else if (!enumerateFile.ToLower().Contains(".development.") && environment?.ToLower() == "development")
                    //{
                    //    continue;
                    //}
                    //else  if (!enumerateFile.ToLower().EndsWith(".json"))
                    //{
                    //    continue;
                    //}

                    var file = new FileInfo(enumerateFile);


                    var files = file.Name.Split(".");
                    if (files.Length == 3 && files[1] == environmentName)
                    {
                        config.AddJsonFile("configs/" + file.Name, true, reloadOnChange);
                    }
                }
            }

            config.AddJsonFile("appsettings.json", true, reloadOnChange);

            Configuration = config.Build();

            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(filePath)
            //    .AddJsonFile(fileName.ToLower() + ".json", true, reloadOnChange);

            //if (environmentName.NotNull())
            //{
            //    builder.AddJsonFile(fileName.ToLower() + "." + environmentName + ".json", optional: true, reloadOnChange: reloadOnChange);
            //}

            //return builder.Build();
        }

        public string GetConfig(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{ToKey(sections)} 不存在", ex);
                //Serilog.Log.Error(ex, $"{ToKey(sections)} 不存在", null);
            }

            return "";
        }

        public T GetSection<T>(params string[] keys) where T : class
        {
            try
            {
                
                if (keys.Any())
                {
                    return Configuration.GetSection(ToKey(keys)).Get<T>();
                }



            }
            catch (Exception ex)
            {
                Logger.Error($"{ToKey(keys)}: 不存在", ex);
            }

            return null;
        }

        private string ToKey(params string[] keys)
        {
            return string.Join(":", keys);
        }
    }
}
