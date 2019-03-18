using log4net.Core;
using log4net.Layout;
using Newtonsoft.Json;
using System.IO;

namespace OptKit.Log4Net.Layout
{
    public class JsonLayout : LayoutSkeleton
    {
        public override void ActivateOptions()
        {

        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            var loggingdata = new
            {
                Demain = loggingEvent.Domain,
                ExceptionString = loggingEvent.ExceptionObject?.ToString(),
                loggingEvent.Identity,
                Level = loggingEvent.Level.Name,
                LocationInfo = new LocationInfo(loggingEvent.LocationInformation?.ClassName, loggingEvent.LocationInformation?.MethodName, loggingEvent.LocationInformation?.FileName, loggingEvent.LocationInformation?.LineNumber),
                loggingEvent.LoggerName,
                Message = JsonConvert.SerializeObject(loggingEvent.MessageObject),
                loggingEvent.ThreadName,
                loggingEvent.TimeStamp,
                loggingEvent.UserName
            };

            var json = JsonConvert.SerializeObject(loggingdata);
            var data = "LoggingData" + json;
            writer.Write(data);
        }
    }
}
