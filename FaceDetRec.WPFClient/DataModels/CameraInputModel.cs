using System;
using System.Windows.Controls;

namespace FaceDetRec.WPFClient.DataModels
{
    public class CameraInputModel : ListBoxItem
    {
        public int Index { get; set; }

        public Guid ClassId { get; set; }
    }
}
