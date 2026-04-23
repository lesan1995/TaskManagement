using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct IssueSeverity
    {
        public int Value { get; init; }
        public string Name { get; init; }
        private IssueSeverity(int value, string name)
        {
            Value = value;
            Name = name;
        }
        public static IssueSeverity Low = new IssueSeverity(0, nameof(Low));
        public static IssueSeverity Medium = new IssueSeverity(1, nameof(Medium));
        public static IssueSeverity High = new IssueSeverity(2, nameof(High));
        public static IssueSeverity From(int value) => value switch
        {
            0 => Low,
            1 => Medium,
            2 => High,
            _ => throw new ArgumentException($"Invalid Issue Severity: {value} ")
        };
        public override string ToString() => Name;
    }
}
