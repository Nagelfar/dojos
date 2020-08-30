using System;
using Xunit;

namespace DojoAug
{
    public class UnitTest1
    {
        [Fact]
        public void When_Creating_a_Wardrobe_a_positive_width_should_be_accepted()
        {
            var wardrobe = Wardrobe.Create(250);

            Assert.Equal(250, wardrobe.width);
        }

        [Fact]
        public void When_Creating_a_Wardrobe_a_negative_width_should_be_rejected()
        {
            Assert.Throws<ApplicationException>(() => Wardrobe.Create(-10));
        }

        [Fact]
        public void When_Creating_a_Element_a_positive_width_should_be_accepted()
        {
            var element = Element.Create(50);

            Assert.Equal(50, element.width);
        }

        [Fact]
        public void wordrobe_should_return_remaining_space()
        {
            var wardrobe = Wardrobe.Create(250);
            Assert.Equal(250, wardrobe.RemainingSpace);
        }

        [Fact]
        public void When_a_Wardrobe_has_one_50_Element_its_RemainingSpace_should_be_reduced_by_50()
        {
            var wardrobe = Wardrobe.Create(250);
            var element = Element.Create(50);
            wardrobe.AddElement(element);
            Assert.Equal(200, wardrobe.RemainingSpace);
        }

        [Fact]
        public void When_a_Wardrobe_has_less_space_than_the_element_needs_it_should_throw_an_exception()
        {
            var wardrobe = Wardrobe.Create(40);
            var element = Element.Create(50);
            Assert.Throws<ApplicationException>(() => wardrobe.AddElement(element));
        }


        [Fact]
        public void When_element_is_smaller_it_should_fit()
        {
            var element = Element.Create(50);
            var fits = element.fits(250);
            Assert.True(fits);
        }

        [Fact]
        public void When_remaining_space_is_smaller_than_element_then_it_should_not_fit()
        {
            var element = Element.Create(50);
            var fits = element.fits(40);
            Assert.False(fits);
        }

        // var catalog = Catalog.Create(element1,element2,..);
        // var options = catalog.GetOptions(wardrobe)


        [Fact]
        public void When_using_a_greedy_strategy_then_the_widest_element_should_be_choosen()
        {
            var el50 = Element.Create(50);
            var elements = new[] { el50, Element.Create(40) };
            var strategy = new GreedyStrategy(elements);
            var wardrobeWidth = 50;
            Wardrobe[] options = strategy.FindMatchingOptions(wardrobeWidth);

            var wardrobe = Wardrobe.Create(50);
            wardrobe.AddElement(el50);
            var expected = new[] { wardrobe };

            Assert.Equal(new[] { el50 }, options[0].Elements);
        }

        [Fact]
        public void When_using_a_greedy_strategy_then_the_secound_element_should_also_be_the_widest_if_it_fits()
        {
            var el50 = Element.Create(50);
            var elements = new[] { el50, Element.Create(40) };
            var strategy = new GreedyStrategy(elements);
            var wardrobeWidth = 100;
            Wardrobe[] options = strategy.FindMatchingOptions(wardrobeWidth);

            var wardrobe = Wardrobe.Create(wardrobeWidth);
            wardrobe.AddElement(el50);
            wardrobe.AddElement(el50);
            var expected = new[] { wardrobe };

            Assert.Equal(new[] { el50, el50 }, options[0].Elements);
        }

        [Fact]
        public void When_using_a_greedy_strategy_and_the_widest_element_is_to_big_then_the_secound_smallest_element_should_be_added()
        {
            var el50 = Element.Create(50);
            var el40 = Element.Create(40);
            var elements = new[] { el50, el40 };
            var strategy = new GreedyStrategy(elements);
            var wardrobeWidth = 49;
            Wardrobe[] options = strategy.FindMatchingOptions(wardrobeWidth);

            var wardrobe = Wardrobe.Create(wardrobeWidth);
            wardrobe.AddElement(el40);
            var expected = new[] { wardrobe };

            Assert.Equal(new[] { el40 }, options[0].Elements);
        }

                [Fact]
        public void When_using_a_greedy_strategy_and_return_the_maximum_for_this_strategy()
        {
            var el50 = Element.Create(50);
            var el20 = Element.Create(20);
            var el10 = Element.Create(10);
            var elements = new[] { el50, el20, el10 };
            var strategy = new GreedyStrategy(elements);
            var wardrobeWidth = 80;
            Wardrobe[] options = strategy.FindMatchingOptions(wardrobeWidth);

            Assert.Equal(new[] { el50, el20, el10 }, options[0].Elements);
        }
    }


}
