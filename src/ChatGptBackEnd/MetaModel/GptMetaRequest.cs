﻿using Newtonsoft.Json;
using ProgrammerToolkit.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptBackEnd.MetaModel
{
    public class GptMetaRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("messages")]
        public List<GptMessage> Messages { get; set; }

        public GptMetaRequest()
        {
            Model = Common.Gpt35Model;
            Messages = new List<GptMessage>();
        }
    }
}
