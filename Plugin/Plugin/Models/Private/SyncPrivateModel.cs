﻿using Plugin.Interfaces;
using Plugin.Schemes;
using Plugin.Templates;

namespace Plugin.Models.Private
{
    public class SyncPrivateModel<T> : BaseModel<T>, IPrivateModel where T : struct
    {

    }
}
