using FaceDetRec.WPFClient.Common.Enums;
using FaceDetRec.WPFClient.ViewModels.MainWindow;
using System.ComponentModel;
using System.Threading.Tasks;

namespace FaceDetRec.WPFClient.Utils
{
    public static class StatusBarUtility
    {
        public static string ChangeStatus(StatusTypes statusType)
        {
            return statusType.GetDesciption();
        }

        public static string GetDesciption<T>(this T source)
        {
            var fi = source.GetType().GetField(source.ToString());
            var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            return attributes.Length > 0
                ? attributes[0].Description
                : source.ToString();
        }

        //todo: przerobic na serwis
        public static void ResestStatus(IControls controls)
        {
            Task.Delay(700).ContinueWith(_ =>
                {
                    controls.StatusText = StatusTypes.Ready.GetDesciption();
                    controls.IsNewUpdate = false;
                }
            );
        }
    }
}
