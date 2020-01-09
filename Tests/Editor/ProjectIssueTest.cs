﻿using NUnit.Framework;
using Unity.ProjectAuditor.Editor;

namespace UnityEditor.ProjectAuditor.EditorTests
{
	class ProjectIssueTest
	{
		[Test]
		public void UninitializedIssueTestPasses()
		{
			var uninitialised = new ProjectIssue(new ProblemDescriptor{}, "dummy issue", IssueCategory.ApiCalls);
			Assert.AreEqual(string.Empty, uninitialised.filename);
			Assert.AreEqual(string.Empty, uninitialised.relativePath);
			Assert.AreEqual(string.Empty, uninitialised.callingMethod);
			Assert.AreEqual(string.Empty, uninitialised.name);
			Assert.AreEqual(string.Empty, uninitialised.name);
		}
	}	
}

