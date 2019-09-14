
using System.Text;
using Newtonsoft.Json;
namespace Devices
{
    internal class DB
    {
        private static HIHSThread thread;

        static DB()
        {
            thread = new HIHSThread();
            System.Windows.Forms.Application.ApplicationExit += (s, o) => Exit();
        }

        private static void Exit()
        {
            thread.Exit();
        }


        public static void Save(Result result)
        {
            //将写件操作添加到队列
            thread.QueueWork(() =>
            {
                //HttpItem hi = new HttpItem { ContentType= "application/json", Url= "http://localhost:5921/api/result", Method=HttpMethod.Post };
                HttpItem hi = new HttpItem { ContentType = "application/json", Url = "http://140.143.203.114:80/api/result", Method = HttpMethod.Post };
                var newresult = new { result.CMD, result.Date, result.ResultDatas, result.Devices.Info };
                string json = JsonConvert.SerializeObject(new
                {
                    OrgCode =1,
                    DoctorCode =result.CMD.DoctorId,
                    DeviceCode = result.Devices.Info.Code,
                    PetCode=result.CMD.PetId,
                    CreateTime=result.Date,
                    ResultType = 0,
                    Content = JsonConvert.SerializeObject(newresult)
                });

                hi.Content = UTF8Encoding.UTF8.GetBytes(json);
                try
                {
                    WebLogic.GetHttpResult<string>(hi);
                }
                catch (System.Exception ex)
                {
                }
            });
        }

    }
}