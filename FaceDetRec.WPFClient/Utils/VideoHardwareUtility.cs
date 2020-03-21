using DirectShowLib;
using System.Collections.ObjectModel;
using System.Linq;
using FaceDetRec.WPFClient.DataModels;

namespace FaceDetRec.WPFClient.Utils
{
    public static class VideoHardwareUtility
    {
        public static ObservableCollection<CameraInputModel> GetCamerasList()
        {
            var camerasList = new ObservableCollection<CameraInputModel>();
            var cameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            var cameraIndex = 0;

            foreach (var camera in cameras)
            {
                camerasList.Add(new CameraInputModel
                {
                    Index = cameraIndex,
                    ClassId = camera.ClassID,
                    Content = camera.Name
                });

                cameraIndex++;
            }

            return camerasList;
        }

        public static CameraInputModel SetDefaultDevice(this ObservableCollection<CameraInputModel> availableCameras)
        {
            return availableCameras.Count > 0 
                ? availableCameras.FirstOrDefault() 
                : null;
        }
    }
}
