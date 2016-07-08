#region License, Terms and Conditions
//
// Maticsoft - JSON and JSON-RPC for Microsoft .NET Framework and Mono
//
// Copyright (c) 2006-2012 Maticsoft. All Rights Reserved.
//
// This library is free software; you can redistribute it and/or modify it under
// the terms of the GNU Lesser General Public License as published by the Free
// Software Foundation; either version 3 of the License, or (at your option)
// any later version.
//
// This library is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
// FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
// details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this library; if not, write to the Free Software Foundation, Inc.,
// 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 
//
#endregion

namespace MyAspx.Json.RPC.Services
{
    #region Imports

    using System;

    #endregion

    public interface IMethodImpl
    {
        bool IsAsynchronous { get; }
        object Invoke(IService service, object[] args);
        IAsyncResult BeginInvoke(IService service, object[] args, AsyncCallback callback, object asyncState);
        object EndInvoke(IService service, IAsyncResult asyncResult);
    }
}
