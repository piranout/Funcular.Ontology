﻿#region File info
// *********************************************************************************************************
// Funcular.Ontology>Funcular.Ontology>Createable.cs
// Created: 2015-07-01 2:16 PM
// Updated: 2015-07-01 2:50 PM
// By: Paul Smith 
// 
// *********************************************************************************************************
// LICENSE: The MIT License (MIT)
// *********************************************************************************************************
// Copyright (c) 2010-2015 <copyright holders>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// *********************************************************************************************************
#endregion


#region Usings
using System;
#endregion


namespace Funcular.Ontology.Archetypes
{
    public abstract class Createable<TId> : ICreateable<TId>
    {
        private TId _createdBy;

        protected Createable()
        {
            if (IdentityFunction != null)
                Id = IdentityFunction();
        }


        #region Implementation of IIdentity
        public virtual TId Id { get; set; }
        #endregion


        #region Implementation of ICreateable
        public virtual DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;
        public virtual TId CreatedBy { get { return this._createdBy; } set { this._createdBy = value; } }
        #endregion


        /// <summary>
        ///     Set this once in your app domain and ID assignment is done
        ///     for all entities.e.g., calling
        /// </summary>
        /// <example>
        /// // Assuming you have an Id generator instance...
        /// Createable.IdentityFunction = () => _generator.NewId();
        /// // Make sure this function does not throw exceptions.
        /// </example>
        public static Func<TId> IdentityFunction { get; set; }
    }
}