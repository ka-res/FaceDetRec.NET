using System.ComponentModel;

namespace FaceDetRec.WPFClient.Common.Enums
{
    public enum StatusTypes
    {
        [Description("Gotowy")]
        Ready,
        [Description("Rozpoczęto przechwytywanie strumienia wideo")]
        StreamStarted,
        [Description("Zakończono przechwytywanie strumienia wideo")]
        StreamPaused,
        [Description("Odnaleziono twarz na ujęciu")]
        FaceDetected,
        [Description("Zapisano w bazie danych")]
        SaveCompleted,
        [Description("Ponowne trenowanie zakończone")]
        RetrainCompleted
    }
}
