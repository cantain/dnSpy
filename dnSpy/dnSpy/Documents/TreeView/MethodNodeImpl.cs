﻿/*
    Copyright (C) 2014-2018 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using dnlib.DotNet;
using dnSpy.Contracts.Decompiler;
using dnSpy.Contracts.Documents.TreeView;
using dnSpy.Contracts.Images;
using dnSpy.Contracts.Text;
using dnSpy.Contracts.TreeView;

namespace dnSpy.Documents.TreeView {
	sealed class MethodNodeImpl : MethodNode {
		public override Guid Guid => new Guid(DocumentTreeViewConstants.METHOD_NODE_GUID);
		public override NodePathName NodePathName => new NodePathName(Guid, MethodDef.FullName);
		protected override ImageReference GetIcon(IDotNetImageService dnImgMgr) => dnImgMgr.GetImageReference(MethodDef);
		public override ITreeNodeGroup TreeNodeGroup { get; }

		public MethodNodeImpl(ITreeNodeGroup treeNodeGroup, MethodDef methodDef)
			: base(methodDef) => TreeNodeGroup = treeNodeGroup;

		protected override void WriteCore(ITextColorWriter output, IDecompiler decompiler, DocumentNodeWriteOptions options) =>
			new NodePrinter().Write(output, decompiler, MethodDef, GetShowToken(options));

		public override FilterType GetFilterType(IDocumentTreeNodeFilter filter) {
			var res = filter.GetResult(MethodDef);
			if (res.FilterType != FilterType.Default)
				return res.FilterType;
			if (Context.Decompiler.ShowMember(MethodDef))
				return FilterType.Visible;
			return FilterType.Hide;
		}
	}
}
