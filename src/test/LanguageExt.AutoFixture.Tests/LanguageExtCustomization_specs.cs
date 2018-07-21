using Ploeh.AutoFixture;
using Shouldly;
using System;
using Xunit;

namespace LanguageExt.AutoFixture.Tests
{
    public class LanguageExtCustomization_specs
    {
        protected IFixture Fixture
        {
            get
            {
                var fixture = new Fixture();
                fixture.LanguageExt();
                return fixture;
            }
        }

        [Fact]
        public void it_creates_SomeT_instance()
        {
            Fixture.Create<Some<int>>().ShouldNotBeNull();
        }

        [Fact]
        public void it_creates_OptionT_instance()
        {
            Fixture.Create<Option<int>>().ShouldNotBeNull();
        }

        [Fact]
        public void it_creates_OptionNone()
        {
            Fixture.Create<OptionNone>();
        }

        [Fact]
        public void it_creates_EitherWithRightState_instance()
        {
            var either = Fixture.Create<Either<int, string>>();

            either.IsBottom.ShouldBeFalse();
            either.IsRight.ShouldBeTrue();
        }

        [Fact]
        public void it_creates_HashSetT_instance()
        {
            Fixture.Create<LanguageExt.HashSet<int>>().ShouldNotBeEmpty();
        }

        [Fact]
        public void it_creates_HashSetT_instance_when_keys_could_be_generated_not_unique()
        {
            var fixture = Fixture;
            fixture.Register<NotUniqueValue>(() => new NotUniqueValue(fixture.Create<int>() % 2));

            fixture.Create<LanguageExt.HashSet<NotUniqueValue>>().ShouldNotBeEmpty();
        }

        [Fact]
        public void it_creates_SetT_instance()
        {
            Fixture.Create<LanguageExt.Set<int>>().ShouldNotBeEmpty();
        }

        [Fact]
        public void it_creates_ArrT_instance()
        {
            Fixture.Create<LanguageExt.Arr<int>>().ShouldNotBeEmpty();
        }

        [Fact]
        public void it_creates_LstT_instance()
        {
            Fixture.Create<LanguageExt.Lst<int>>().ShouldNotBeEmpty();
        }

        [Fact]
        public void it_creates_MapT_instance()
        {
            Fixture.Create<LanguageExt.Map<int, int>>().ShouldNotBeEmpty();
        }


        [Fact]
        public void it_creates_HashMapT_instance()
        {
            Fixture.Create<LanguageExt.HashMap<int, int>>().ShouldNotBeEmpty();
        }

        [Fact]
        public void it_creates_HashMapT_instance_when_keys_could_be_generated_not_unique()
        {
            var fixture = Fixture;
            fixture.Register<NotUniqueValue>(() => new NotUniqueValue(fixture.Create<int>() % 2));

            fixture.Create<LanguageExt.HashMap<NotUniqueValue, int>>().ShouldNotBeEmpty();
        }

        [Fact]
        public void it_creates_QueT_instance()
        {
            Fixture.Create<LanguageExt.Que<int>>().ShouldNotBeEmpty();
        }

        [Fact]
        public void it_creates_StckT_instance()
        {
            Fixture.Create<LanguageExt.Stck<int>>().ShouldNotBeEmpty();
        }
    }

    class NotUniqueValue : NewType<NotUniqueValue, int>
    {
        public NotUniqueValue(int value) : base(value)
        {
        }
    }
}
