using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Ploeh.AutoFixture.Xunit2;
using Ploeh.SemanticComparison;
using Ploeh.SemanticComparison.Fluent;
using Xunit;

namespace SemanticComparison
{
    public class SemanticComparisonTheories
    {
        [Theory, AutoData]
        public void ListsWithSameElementsAreNotEqual(
            List<int> expected)
        {
            var actual = expected.ToList();

            Action action = () => actual.AsSource().OfLikeness<List<int>>().ShouldEqual(expected);

            action.ShouldThrow<LikenessException>();
        }

        public class SimpleCollection
        {
            public List<int> Ints { get; set; }    
        }

        [Theory, AutoData]
        public void ObjectsWithListPropertiesWithSameSimpleElementsCanBeMadeEqual(
            SimpleCollection expected)
        {
            var actual = new SimpleCollection
            {
                Ints = expected.Ints.ToList()
            };

            var likeness = actual.AsSource().OfLikeness<SimpleCollection>()
                .With(x => x.Ints).EqualsWhen(((a, b) => a.Ints.SequenceEqual(b.Ints)));

            likeness.ShouldEqual(actual);
        }

        public class ComplexCollection
        {
            public List<ComplexElement> Elements { get; set; } 
        }

        public class ComplexElement
        {
            public int Property { get; set; }
        }

        [Theory, AutoData]
        public void ObjectsWithListPropertiesWithSameComplexElementsCanBeMadeEqual(
            ComplexCollection expected)
        {
            var actual = new ComplexCollection
            {
                Elements = expected.Elements.Select(x => new ComplexElement {Property = x.Property}).ToList()
            };

            var likeness = actual.AsSource().OfLikeness<ComplexCollection>()
                .With(x => x.Elements).EqualsWhen(
                    (a, b) =>
                        a.Elements.Select(x => x.AsSource().OfLikeness<ComplexElement>().CreateProxy()).ToList()
                            .SequenceEqual(
                                b.Elements.Select(x => x.AsSource().OfLikeness<ComplexElement>().CreateProxy()).ToList())
            );

            likeness.ShouldEqual(expected);
        }

        public class HasAnObjectProperty
        {
            public ComplexCollection ComplexCollection { get; set; }
        }

        [Theory, AutoData]
        public void ObjectsWithEqualObjectPropertiesCanBeMadeEqual(
            HasAnObjectProperty expected)
        {
            var actual = new HasAnObjectProperty
            {
                ComplexCollection = expected.ComplexCollection
            };

            var likeness = actual.AsSource().OfLikeness<HasAnObjectProperty>();
            likeness.ShouldEqual(expected);
        }

        [Theory, AutoData]
        public void ObjectsWithEqualListPropertiesCanBeMadeEqual(
            ComplexCollection expected)
        {
            var actual = new ComplexCollection
            {
                Elements = expected.Elements
            };

            var likeness = actual.AsSource().OfLikeness<ComplexCollection>();
            likeness.ShouldEqual(actual);
        }
    }
}
