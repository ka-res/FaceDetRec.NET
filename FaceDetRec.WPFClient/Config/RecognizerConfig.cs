using System;
using System.IO;
using System.Reflection;

namespace FaceDetRec.WPFClient.Config
{
    public static class RecognizerConfig
    {
        public const string FaceDetRec =
            "FaceDetRec";
        public const string Data =
            "Data";
        public const string People =
            "People";
        public const string Images =
            "Images";
        public const string Person =
            "person";
        public const string Image =
            "image";
        public const string DefaultFileExtension =
            "dat";
        public const string DefaultImageExtension =
            "jpg";
        public const string LocalStorage =
            "LocalStorage";
        public const string LocalStorageSuffix =
            "LS";
        public const string DatabaseSuffix =
            "DB";

        /// <summary>
        /// Local people.dat (if chosen) filename
        /// </summary>
        public static readonly string PeopleFileName =
            $"{People.ToLower()}.{DefaultFileExtension}";

        /// <summary>
        /// Application main filepath
        /// </summary>
        public static readonly string FaceDetRecPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            FaceDetRec);

        /// <summary>
        /// Local storage (if chosen) direcotry path
        /// </summary>
        public static readonly string LocalStoragePath =
            Path.Combine(FaceDetRecPath, LocalStorage);

        /// <summary>
        /// Local images (if chosen) directory path
        /// </summary>
        public static readonly string ImagesPath =
            Path.Combine(LocalStoragePath, Images);

        /// <summary>
        /// Local people (if chosen) directory path
        /// </summary>
        public static readonly string PeoplePath =
            Path.Combine(LocalStoragePath, People);

        /// <summary>
        /// Local people.dat (if chosen) file path
        /// </summary>
        public static readonly string PeopleFilePath =
            Path.Combine(PeoplePath, PeopleFileName);

        /// <summary>
        /// Eigen Recognizer config file path
        /// </summary>
        public static readonly string EigenLocalConfigFileName =
            $"EigenTrainResult_{LocalStorageSuffix}.{DefaultFileExtension}";

        /// <summary>
        /// Eigen Recognizer config file path
        /// </summary>
        public static readonly string EigenDatabaseConfigFileName =
            $"EigenTrainResult_{DatabaseSuffix}.{DefaultFileExtension}";

        /// <summary>
        /// Fisher Recognizer config file path
        /// </summary>
        public static readonly string FisherLocalConfigFileName =
            $"FisherTrainResult_{LocalStorageSuffix}.{DefaultFileExtension}";

        /// <summary>
        /// Fisher Recognizer config file path
        /// </summary>
        public static readonly string FisherDatabaseConfigFileName =
            $"FisherTrainResult_{DatabaseSuffix}.{DefaultFileExtension}";

        /// <summary>
        /// LBPH Recognizer config file path
        /// </summary>
        public static readonly string LbphDatabaseConfigFileName =
            $"LbphTrainResult_{DatabaseSuffix}.{DefaultFileExtension}";

        /// <summary>
        /// LBPH Recognizer config file path
        /// </summary>
        public static readonly string LbphLocalConfigFileName =
            $"LbphTrainResult_{LocalStorageSuffix}.{DefaultFileExtension}";

        /// <summary>
        /// Data filepath
        /// </summary>
        public static readonly string DataPath = Path.Combine(
            FaceDetRecPath, 
            Data);

        /// <summary>
        /// Eigen recognizer settings filepath
        /// </summary>
        public static readonly string EigenLocalConfigurationPath = Path.Combine(
            FaceDetRecPath, EigenLocalConfigFileName);

        /// <summary>
        /// Eigen recognizer settings filepath
        /// </summary>
        public static readonly string EigenDatabaseConfigurationPath = Path.Combine(
            FaceDetRecPath, EigenDatabaseConfigFileName);

        /// <summary>
        /// Eigen recognizer settings filepath
        /// </summary>
        public static readonly string FisherLocalConfigurationPath = Path.Combine(
            FaceDetRecPath, FisherLocalConfigFileName);

        /// <summary>
        /// Eigen recognizer settings filepath
        /// </summary>
        public static readonly string FisherDatabaseConfigurationPath = Path.Combine(
            FaceDetRecPath, FisherDatabaseConfigFileName);

        /// <summary>
        /// Eigen recognizer settings filepath
        /// </summary>
        public static readonly string LbphDatabaseConfigurationPath = Path.Combine(
            FaceDetRecPath, LbphDatabaseConfigFileName);

        /// <summary>
        /// Eigen recognizer settings filepath
        /// </summary>
        public static readonly string LbphLocalConfigurationPath = Path.Combine(
            FaceDetRecPath, LbphLocalConfigFileName);

        public static readonly string CascadeFaceFilePath =
            Path.Combine(
                path1: Path.GetDirectoryName(
                           path: Assembly.GetExecutingAssembly().Location)
                       ?? throw new InvalidOperationException(),
                path2: @"Assets\haarcascade_frontalface_alt_tree.xml");

        public static readonly string CascadeEyeFilePath =
            Path.Combine(
                path1: Path.GetDirectoryName(
                           path: Assembly.GetExecutingAssembly().Location)
                       ?? throw new InvalidOperationException(),
                path2: @"Assets\haarcascade_eye.xml");
    }
}
