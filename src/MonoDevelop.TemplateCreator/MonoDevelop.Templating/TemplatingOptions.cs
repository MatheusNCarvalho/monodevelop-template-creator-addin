﻿//
// TemplatingOptions.cs
//
// Author:
//       Matt Ward <matt.ward@xamarin.com>
//
// Copyright (c) 2017 Xamarin Inc. (http://xamarin.com)
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

using System;
using System.Collections.Generic;
using System.Linq;
using MonoDevelop.Core;

namespace MonoDevelop.Templating
{
	class TemplatingOptions
	{
		static readonly string TemplateFoldersPropertyName = "TemplateFolders";

		List<string> templateFolders = new List<string> ();

		public TemplatingOptions ()
		{
			templateFolders = PropertyService.Get (TemplateFoldersPropertyName, new List<string> ());
		}

		public IEnumerable<string> TemplateFolders {
			get { return templateFolders; }
		}

		public void UpdateTemplateFolders (IEnumerable<string> folders)
		{
			if (!FoldersHaveChanged (folders))
				return;

			templateFolders = folders.ToList ();
			Save ();

			TemplatingServices.EventsService.OnTemplateFoldersChanged ();
		}

		bool FoldersHaveChanged (IEnumerable<string> folders)
		{
			return !folders.SequenceEqual (templateFolders);
		}

		void Save ()
		{
			PropertyService.Set (TemplateFoldersPropertyName, templateFolders);
			PropertyService.SaveProperties ();
		}

		public void AddTemplateFolder (string templateFolder)
		{
			if (!templateFolders.Contains (templateFolder)) {
				templateFolders.Add (templateFolder);
				TemplatingServices.EventsService.OnTemplateFoldersChanged ();
			}
		}
	}
}
