﻿using Newtonsoft.Json;
using Oazachaosu.Core.Common;
using System.Collections.Generic;
using Wordki.Helpers.AutoMapper;
using WordkiModel;

namespace Wordki.Helpers.Connector.Requests
{
    public class PostWordsRequest : ApiRequestBase
    {
        protected override string Path { get { return "Words/"; } }

        public PostWordsRequest(IUser user, IEnumerable<IWord> words) : base(user)
        {
            Method = "POST";
            Message = JsonConvert.SerializeObject(new { user.ApiKey, Data = AutoMapperConfig.Instance.Map<IEnumerable<IWord>, IEnumerable<WordDTO>>(words) });
        }
    }
}
