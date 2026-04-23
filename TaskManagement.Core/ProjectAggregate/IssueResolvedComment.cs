using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct IssueResolvedComment
    {
        public string Value { get; init; }
        public const int MaxLength = 100;
        private IssueResolvedComment(string value) => Value = value;
        public static IssueResolvedComment Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Issue Title cannot be empty.");
            if (value.Length < MaxLength)
                throw new ArgumentException($"Issue title cannot be more than {MaxLength} characters");
            return new IssueResolvedComment(value);
        }
        public override string ToString() => Value;
    }
}
