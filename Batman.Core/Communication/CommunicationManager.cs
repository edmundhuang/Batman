﻿/*
Copyright (c) 2013 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using Batman.Core.Communication.BaseClasses;
using Batman.Core.Communication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Utilities.DataTypes.ExtensionMethods;

namespace Batman.Core.Communication
{
    /// <summary>
    /// Communication manager (Email, SMS, Twitter, etc.)
    /// </summary>
    public class CommunicationManager
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CommunicationManager()
        {
            Formatters = AppDomain.CurrentDomain.GetAssemblies().Objects<IFormatter>();
            Communicators = new Dictionary<Type, ICommunicator>();
            AppDomain.CurrentDomain.GetAssemblies().Objects<CommunicatorBase>().ForEach(x => { x.Initialize(Formatters); Communicators.Add(x.MessageType, x); });
        }

        /// <summary>
        /// Communicators
        /// </summary>
        public IDictionary<Type, ICommunicator> Communicators { get; private set; }

        /// <summary>
        /// Formatters
        /// </summary>
        public IEnumerable<IFormatter> Formatters { get; private set; }

        /// <summary>
        /// Gets the communicator by its message type
        /// </summary>
        /// <param name="MessageType">Message type</param>
        /// <returns>The communicator based on its message type</returns>
        public ICommunicator this[Type MessageType] { get { return Communicators[MessageType]; } }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return new StringBuilder()
                        .AppendLine()
                        .AppendLineFormat("Formatters: {0}", Formatters.ToString(x => x.Name))
                        .AppendFormat("Communicators: {0}", Communicators.ToString(x => x.Value.Name))
                        .ToString();
        }
    }
}