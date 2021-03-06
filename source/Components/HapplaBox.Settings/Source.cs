using HapplaBox.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace HapplaBox.Settings
{
    public class Source
    {

        #region Public properties

        /// <summary>
        /// Gets the user config file name.
        /// </summary>
        public static string UserFilename => "HapplaBoxConfig.user.json";


        /// <summary>
        /// Gets the default config file located.
        /// </summary>
        public static string DefaultFilename => "HapplaBoxConfig.default.json";


        /// <summary>
        /// Gets the admin config file name.
        /// </summary>
        public static string AdminFilename => "HapplaBoxConfig.admin.json";


        /// <summary>
        /// Config file description
        /// </summary>
        public string Description { get; set; } = "HapplaBox configuration file";


        /// <summary>
        /// Config file version
        /// </summary>
        public string Version { get; set; } = "1.0";


        /// <summary>
        /// Gets, sets value indicates that the config file is compatible with this HapplaBox version or not
        /// </summary>
        public bool IsCompatible { get; set; } = true;


        #endregion




        #region Public methods


        /// <summary>
        /// Loads all config files: default, user, command-lines, admin;
        /// then unify configs.
        /// </summary>
        public IConfigurationRoot LoadUserConfigs()
        {
            // filter the command lines begin with '-'
            // example: HapplaBox.exe -WinMainWidth=900
            var args = Environment.GetCommandLineArgs()
                .Where(cmd => cmd.StartsWith('-'))
                .Select(cmd => cmd[1..]) // trim '-' from the command
                .ToArray();

            var userConfig = new ConfigurationBuilder()
              .SetBasePath(MyApp.ConfigDir(PathType.Dir))
              .AddJsonFile(DefaultFilename, optional: true)
              .AddJsonFile(UserFilename, optional: true)
              .AddCommandLine(args)
              .AddJsonFile(AdminFilename, optional: true)
              .Build();

            return userConfig;
        }


        #endregion


    }
}
