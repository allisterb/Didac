﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLAF.Services.Storage
{
    public class DocumentLogEvent : BaseLogEvent
    {
        public DocumentLogEvent() { }
        public DocumentLogEvent(TextArtifact artifact) : base(artifact)
        {
            Name = artifact.HasFileSource ? System.IO.Path.GetFileName(artifact.Name) : artifact.Name;
            PotentialSensitiveData = artifact.HasSensitiveData ? artifact.SensitiveData.Values.Aggregate((p, n) => p + "," + n) : "";
            KeyWords = string.Join(",", artifact.KeyWords.ToArray());
            Entities = string.Join(",", artifact.Entities.ToArray());
            Global.Logger.Debug("Created Azure Log Analytics log event {0} for text artifact {1} at {2}.", Name, artifact.Id, DateTime.Now);
        }

        public DocumentLogEvent(ImageArtifact artifact) : base(artifact)
        {
            IsImage = true;
            Name = artifact.HasFileSource ? System.IO.Path.GetFileName(artifact.Name) : artifact.Name;
            Categories = string.Join(",", artifact.Categories.Select(c => c.Name).ToArray());
            Tags = string.Join(",", artifact.Tags);
            IsAdultImage = artifact.IsAdultContent || artifact.IsRacy;
            Global.Logger.Debug("Created Azure Log Analytics log event {0} for image artifact {1} at {2}.", Name, artifact.Id, DateTime.Now);
        }

        public string Name { get; set; }

        public string FilePath { get; set; }

        public bool IsImage { get; set; }

        public string PotentialSensitiveData { get; set; }

        public string KeyWords { get; set; }

        public string Entities { get; set; }

        public string Categories { get; set; }

        public string Tags { get; set; }

        public bool IsAdultImage {get; set;}
        
        public string Caption { get; set; }

    }
}
