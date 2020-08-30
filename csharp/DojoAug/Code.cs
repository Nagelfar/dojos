using System;
using System.Collections.Generic;
using System.Linq;

namespace DojoAug
{
    public class Wardrobe
    {
        private Wardrobe(int width)
        {
            this.width = width;
        }

        public int width
        {
            get;
        }



        public List<Element> Elements = new List<Element>();
        public int RemainingSpace => this.width - this.Elements.Sum(x => x.width);

        public void AddElement(Element element)
        {
            if (!element.fits(RemainingSpace))
                throw new ApplicationException();

            this.Elements.Add(element);
        }

        public static Wardrobe Create(int width)
        {
            if (width < 0)
                throw new ApplicationException();

            var wardrobe = new Wardrobe(width);
            return wardrobe;
        }
    }

    public class Element
    {
        private Element(int width)
        {
            this.width = width;
        }

        public int width
        {
            get;
        }
        public static Element Create(int width)
        {
            var element = new Element(width);
            return element;
        }

        public bool fits(int remainingWidth) => remainingWidth >= this.width;
    }



    public class GreedyStrategy
    {

        private Element[] elements;

        public GreedyStrategy(Element[] elements)
        {
            this.elements = elements;
        }

        private Element FindNextFittingElement(Wardrobe wardrobe)
        {
            var orderedElements = elements.OrderByDescending(x => x.width);
            foreach (var el in orderedElements)
            {
                if (el.fits(wardrobe.RemainingSpace))
                {
                    return el;
                }
            }
            return null;
        }

        private bool TryAddElement(Wardrobe wardrobe)
        {
            var nextElement = FindNextFittingElement(wardrobe);
            if (nextElement != null)
            {
                wardrobe.AddElement(nextElement);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Wardrobe[] FindMatchingOptions(int wardrobeWidth)
        {
            var wardrobe = Wardrobe.Create(wardrobeWidth);
            var added = false;
            do
            {
                added = TryAddElement(wardrobe);
            } while (added);

            return new[] { wardrobe };
        }

    }
}