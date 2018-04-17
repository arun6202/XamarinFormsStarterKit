using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Bogus.DataSets;
using System.Reflection;
using System.Linq;
using FFImageLoading.Svg.Forms;

namespace XamarinFormsStarterKit.LayoutGallery.VisualElementColorizer
{
    public static class Colorizer
    {

        static Colorizer()
        {
            colorizeList = new List<Color>(colorList);
            colorizeLightList = new List<Color>(colorList);
            colorizeDarkList = new List<Color>(colorList);

            loremTextList = new List<Color>(colorList);
            loremImageList = new List<Color>(colorList);

            svgImageList = new List<string>
    {
horizontalstripes,
greenstripes,
honeycomb,
chevrons,
carbonfiber,
microbialmat,
dance,
checkerboard,
waves,
verticalstripes,
shippo,
halfrombes,
transparent,
simplehorizontal,
whitecarbon,
crossstripes,
subtledots,
thinstripes,
specklednoise,
blueprintgrid,
argyle,


    };
        }


		public static void CompressedLayoutHeadless(Layout layout, bool apply = true)
        {

 
            if (!apply)
            {
                return;
            }


            
            foreach (var child in layout.Children)
            {
                if (child is Layout currentLayout)
                {
                    CompressedLayoutHeadless(currentLayout);
                }
                 
                if (child is Layout currentLayoutHeadless)
                {
                    CompressedLayout.SetIsHeadless(child,true);

                }
                 
            }
        }

        static List<string> svgImageList;

        public static void RandomImage(Layout layout, bool apply = true)
        {
#if DEBUG

#else
            return;
#endif

            if (!apply)
            {
                return;
            }


            foreach (var child in layout.Children)
            {
                if (child is Layout currentLayout)
                {
                    RandomImage(currentLayout);
                }
                var currentControl = (VisualElement)child;

                if (child is SvgCachedImage img)
                {
                    var imageControl = (VisualElement)child;
                    imageControl.BackgroundColor = Color.Transparent;


                    var source = img.Source.ToString();
                    source = source.Replace("File:", "").ToLower().Trim();
                    double height = 40;
                    double width = 40;
                    switch (source)
                    {
						case "fs":
							height = 768;
							width = 1024;
                            break;
						
                        case "xxxs":
                            height = 10;
                            width = 10;
                            break;

                        case "xxs":
                            height = 20;
                            width = 20;
                            break;
                        case "xs":
                            height = 30;
                            width = 30;
                            break;
                        case "sm":
                            height = 40;
                            width = 40;
                            break;
                        case "md":
                            height = 60;
                            width = 60;
                            break;
                        case "lg":
                            height = 80;
                            width = 80;
                            break;
                        case "xl":
                            height = 100;
                            width = 100;
                            break;
                        case "xxl":
                            height = 120;
                            width = 120;
                            break;
                        case "xxxl":
                            height = 200;
                            width = 200;
                            break;

                        default:
                            if (source.Trim() != string.Empty)
                            {
                                var dimensions = source.Split(new string[] { "/" }, StringSplitOptions.None);

                                if (Int32.TryParse(dimensions[0], out int localwidth))
                                {
                                    currentControl.WidthRequest = localwidth;
                                }

                                if (Int32.TryParse(dimensions[1], out int localheight))
                                {
                                    currentControl.HeightRequest = localheight;
                                }

                            }
                            else
                            {
                                height = 40;
                                width = 40;

                            }

                            break;
                    }
                    img.Source = RandomImage(svgImageList);
                    img.HeightRequest = height;
                    img.WidthRequest = width;

                }

            }


        }

        static List<Color> colorizeList;
        static List<Color> loremTextList;
        static List<Color> loremImageList;
        static List<Color> colorizeLightList;

        static List<Color> colorizeDarkList;


        static readonly Lorem lorem = new Lorem();

        static string RandomImage(List<string> image)
        {
            if (image.Count == 0)
            {
                return "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI2MCIgaGVpZ2h0PSI5MCI+CjxnIHRyYW5zZm9ybT0ic2NhbGUoMSAxLjUpIj4KPHJlY3Qgd2lkdGg9Ijk5IiBoZWlnaHQ9Ijk5IiBmaWxsPSIjNmQ2OTVjIj48L3JlY3Q+CjxyZWN0IHdpZHRoPSI0Mi40MiIgaGVpZ2h0PSI0Mi40MiIgdHJhbnNmb3JtPSJ0cmFuc2xhdGUoMzAgMCkgcm90YXRlKDQ1KSIgZmlsbD0iIzYyNWY1MyI+PC9yZWN0Pgo8cmVjdCB3aWR0aD0iOTkiIGhlaWdodD0iMSIgdHJhbnNmb3JtPSJyb3RhdGUoNDUpIiBmaWxsPSIjNzE2ZjY0Ij48L3JlY3Q+CjxyZWN0IHdpZHRoPSI5OSIgaGVpZ2h0PSIxIiB0cmFuc2Zvcm09InRyYW5zbGF0ZSgwIDYwKSByb3RhdGUoLTQ1KSIgZmlsbD0iIzcxNmY2NCI+PC9yZWN0Pgo8L2c+Cjwvc3ZnPg==";
            }
            var randomIndex = new Random().Next(image.Count);
            var img = image[randomIndex];
            image.RemoveAt(randomIndex);
            return img;
        }

        static Color RandomColor(List<Color> colors)
        {
            if (colors.Count == 0)
            {
                return Color.White; // to do all viable colors are emptied , so please try different layout
            }
            var randomIndex = new Random().Next(colors.Count);
            var color = colorList[randomIndex];
            colors.RemoveAt(randomIndex);
            return color;
        }

        public static void Colorize(Layout layout, bool apply = true)
        {

#if DEBUG

#else
return;
#endif

            if (!apply)
            {
                return;
            }


            layout.BackgroundColor = RandomColor(colorizeList);

            foreach (var child in layout.Children)
            {
                if (child is Layout currentLayout)
                {
                    Colorize(currentLayout);
                }
                var currentControl = (VisualElement)child;
                if (child is Layout)
                {
                    currentControl.BackgroundColor = RandomColor(colorizeList);

                }
                else
                {
                    currentControl.BackgroundColor = RandomColor(colorizeList);

                }
            }
        }




        public static void LoremText(Layout layout, bool apply = true)
        {

#if DEBUG

#else
return;
#endif

            if (!apply)
            {
                return;
            }

            foreach (var child in layout.Children)
            {
                if (child is Layout currentLayout)
                {
                    LoremText(currentLayout);
                }

                if (child.GetType().GetTypeInfo().GetDeclaredProperty("Text") != null)
                {
                    var currentControl = (VisualElement)child;
                    currentControl.BackgroundColor = Color.Transparent;

                    if ((child is Label label))
                    {
                        var text = label.Text.ToLower();

                        text = GenerateLoremText(text);

                        label.Text = text;
                        label.TextColor = RandomColor(loremTextList);
                    }

                    if ((child is Span span))
                    {
                        var text = span.Text.ToLower();

                        text = GenerateLoremText(text);

                        span.Text = text;
                        span.TextColor = RandomColor(loremTextList);
                    }

                    if ((child is Entry entry))
                    {
                        var text = entry.Text.ToLower();

                        text = GenerateLoremText(text);

                        entry.Text = text;
                        entry.TextColor = RandomColor(loremTextList);
                    }

                    if ((child is Editor editor))
                    {
                        var text = editor.Text.ToLower();

                        text = GenerateLoremText(text);

                        editor.Text = text;
                        editor.TextColor = RandomColor(loremTextList);
                    }

                }


            }
        }

        private static string GenerateLoremText(string text)
        {
            if (text.StartsWith("w", StringComparison.CurrentCulture))
            {
                text = text.Replace("w", string.Empty);
                if (text.Length == 0)
                {
                    text = lorem.Word();
                }
                else
                {

                    if (Int32.TryParse(text, out int number))
                    {
                        text = string.Join(" ", lorem.Words(number));
                    }
                }

            }

            if (text.StartsWith("l", StringComparison.CurrentCulture))
            {
                text = text.Replace("l", string.Empty);
                if (text.Length == 0)
                {
                    text = lorem.Lines();
                }
                else
                {
                    if (Int32.TryParse(text, out int number))
                    {
                        text = string.Join(" ", lorem.Lines(number));
                    }
                }
            }
            if (text.StartsWith("p", StringComparison.CurrentCulture))
            {
                text = text.Replace("p", string.Empty);
                if (text.Length == 0)
                {
                    text = lorem.Paragraph();
                }
                else
                {

                    if (Int32.TryParse(text, out int number))
                    {
                        text = string.Join(" ", lorem.Paragraph(number));
                    }
                }
            }
            if (text.StartsWith("t", StringComparison.CurrentCulture))
            {
                text = text.Replace("t", string.Empty);
                if (text.Length == 0)
                {
                    text = lorem.Text();
                }

            }
            if (text.StartsWith("sl", StringComparison.CurrentCulture))
            {
                text = text.Replace("sl", string.Empty);
                if (text.Length == 0)
                {
                    text = lorem.Slug();
                }
                else
                {

                    if (Int32.TryParse(text, out int number))
                    {
                        text = string.Join(" ", lorem.Slug(number));
                    }
                }
            }

            return text;
        }


        public static string horizontalstripes = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI5MCIgaGVpZ2h0PSIzMCI+CjxyZWN0IHdpZHRoPSI5MCIgaGVpZ2h0PSIzMCIgZmlsbD0iIzAwYTlmMSI+PC9yZWN0Pgo8cmVjdCB3aWR0aD0iOTAiIGhlaWdodD0iMTgiIGZpbGw9IiMyNmJhZjQiPjwvcmVjdD4KPC9zdmc+";
        public static string greenstripes = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI3MCIgaGVpZ2h0PSI3MCI+CjxyZWN0IHdpZHRoPSI3MCIgaGVpZ2h0PSI3MCIgZmlsbD0iI2JiZDgxNyI+PC9yZWN0Pgo8ZyB0cmFuc2Zvcm09InJvdGF0ZSg0NSkiPgo8cmVjdCB3aWR0aD0iOTkiIGhlaWdodD0iMjUiIGZpbGw9IiNhOWNlMDAiPjwvcmVjdD4KPHJlY3QgeT0iLTUwIiB3aWR0aD0iOTkiIGhlaWdodD0iMjUiIGZpbGw9IiNhOWNlMDAiPjwvcmVjdD4KPC9nPgo8L3N2Zz4=";
        public static string honeycomb = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI1NiIgaGVpZ2h0PSIxMDAiPgo8cmVjdCB3aWR0aD0iNTYiIGhlaWdodD0iMTAwIiBmaWxsPSIjZjhkMjAzIj48L3JlY3Q+CjxwYXRoIGQ9Ik0yOCA2NkwwIDUwTDAgMTZMMjggMEw1NiAxNkw1NiA1MEwyOCA2NkwyOCAxMDAiIGZpbGw9Im5vbmUiIHN0cm9rZT0iI2ZmZjYyOSIgc3Ryb2tlLXdpZHRoPSIyIj48L3BhdGg+CjxwYXRoIGQ9Ik0yOCAwTDI4IDM0TDAgNTBMMCA4NEwyOCAxMDBMNTYgODRMNTYgNTBMMjggMzQiIGZpbGw9Im5vbmUiIHN0cm9rZT0iI2ZmZTUwMyIgc3Ryb2tlLXdpZHRoPSIyIj48L3BhdGg+Cjwvc3ZnPg==";
        public static string chevrons = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIiB3aWR0aD0iNjAiIGhlaWdodD0iMzAiPgo8ZGVmcz4KPHJlY3QgaWQ9InIiIHdpZHRoPSIzMCIgaGVpZ2h0PSIxNSIgZmlsbD0iI2JiMDg1ZiIgc3Ryb2tlLXdpZHRoPSIyLjUiIHN0cm9rZT0iIzdhMDU0ZCI+PC9yZWN0Pgo8ZyBpZD0icCI+Cjx1c2UgeGxpbms6aHJlZj0iI3IiPjwvdXNlPgo8dXNlIHk9IjE1IiB4bGluazpocmVmPSIjciI+PC91c2U+Cjx1c2UgeT0iMzAiIHhsaW5rOmhyZWY9IiNyIj48L3VzZT4KPHVzZSB5PSI0NSIgeGxpbms6aHJlZj0iI3IiPjwvdXNlPgo8L2c+CjwvZGVmcz4KPHVzZSB4bGluazpocmVmPSIjcCIgdHJhbnNmb3JtPSJ0cmFuc2xhdGUoMCAtMjUpIHNrZXdZKDQwKSI+PC91c2U+Cjx1c2UgeGxpbms6aHJlZj0iI3AiIHRyYW5zZm9ybT0idHJhbnNsYXRlKDMwIDApIHNrZXdZKC00MCkiPjwvdXNlPgo8L3N2Zz4=";
        public static string carbonfiber = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxNSIgaGVpZ2h0PSIxNSI+CjxyZWN0IHdpZHRoPSI1MCIgaGVpZ2h0PSI1MCIgZmlsbD0iIzI4MjgyOCI+PC9yZWN0Pgo8Y2lyY2xlIGN4PSIzIiBjeT0iNC4zIiByPSIxLjgiIGZpbGw9IiMzOTM5MzkiPjwvY2lyY2xlPgo8Y2lyY2xlIGN4PSIzIiBjeT0iMyIgcj0iMS44IiBmaWxsPSJibGFjayI+PC9jaXJjbGU+CjxjaXJjbGUgY3g9IjEwLjUiIGN5PSIxMi41IiByPSIxLjgiIGZpbGw9IiMzOTM5MzkiPjwvY2lyY2xlPgo8Y2lyY2xlIGN4PSIxMC41IiBjeT0iMTEuMyIgcj0iMS44IiBmaWxsPSJibGFjayI+PC9jaXJjbGU+Cjwvc3ZnPg==";
        public static string microbialmat = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIyMCIgaGVpZ2h0PSIyMCI+CjxyZWN0IHdpZHRoPSI0MCIgaGVpZ2h0PSI0MCIgZmlsbD0iIzhhMyI+PC9yZWN0Pgo8Y2lyY2xlIHI9IjkuMiIgc3Ryb2tlLXdpZHRoPSIxIiBzdHJva2U9IiM2MTMiIGZpbGw9Im5vbmUiPjwvY2lyY2xlPgo8Y2lyY2xlIGN5PSIxOC40IiByPSI5LjIiIHN0cm9rZS13aWR0aD0iMXB4IiBzdHJva2U9IiM2MTMiIGZpbGw9Im5vbmUiPjwvY2lyY2xlPgo8Y2lyY2xlIGN4PSIxOC40IiBjeT0iMTguNCIgcj0iOS4yIiBzdHJva2Utd2lkdGg9IjEiIHN0cm9rZT0iIzYxMyIgZmlsbD0ibm9uZSI+PC9jaXJjbGU+Cjwvc3ZnPg==";
        public static string dance = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI4IiBoZWlnaHQ9IjgiPjxwYXRoIGQ9Ik0tMiAxMEwxMCAtMlpNMTAgNkw2IDEwWk0tMiAyTDIgLTIiIHN0cm9rZT0iIzIyMiIgc3Ryb2tlLXdpZHRoPSI0LjUiPjwvcGF0aD4KPC9zdmc+'),url('data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxMDAlIiBoZWlnaHQ9IjEwMCUiPgo8bGluZWFyR3JhZGllbnQgaWQ9ImciIHgyPSIxIiB5Mj0iMSI+CjxzdG9wIHN0b3AtY29sb3I9IiNGMTkiPjwvc3RvcD4KPHN0b3Agb2Zmc2V0PSIxMDAlIiBzdG9wLWNvbG9yPSIjMENGIj48L3N0b3A+CjwvbGluZWFyR3JhZGllbnQ+CjxyZWN0IHdpZHRoPSIxMDAlIiBoZWlnaHQ9IjEwMCUiIGZpbGw9InVybCgjZykiPjwvcmVjdD4KPC9zdmc+";
        public static string checkerboard = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI2MCIgaGVpZ2h0PSI2MCI+CjxyZWN0IHdpZHRoPSI2MCIgaGVpZ2h0PSI2MCIgZmlsbD0iI2ZmZiI+PC9yZWN0Pgo8cmVjdCB3aWR0aD0iNDIuNDIiIGhlaWdodD0iNDIuNDIiIHRyYW5zZm9ybT0idHJhbnNsYXRlKDMwIDApIHJvdGF0ZSg0NSkiIGZpbGw9IiM0NDQiPjwvcmVjdD4KPC9zdmc+";
        public static string waves = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI3NSIgaGVpZ2h0PSIxMDAiPgo8cmVjdCB3aWR0aD0iNzUiIGhlaWdodD0iMTAwIiBmaWxsPSJzbGF0ZWdyYXkiPjwvcmVjdD4KPGNpcmNsZSBjeD0iNzUiIGN5PSI1MCIgcj0iMjguMyUiIHN0cm9rZS13aWR0aD0iMTIiIHN0cm9rZT0iIzlhYTZiMiIgZmlsbD0ibm9uZSI+PC9jaXJjbGU+CjxjaXJjbGUgY3g9IjAiIHI9IjI4LjMlIiBzdHJva2Utd2lkdGg9IjEyIiBzdHJva2U9IiM5YWE2YjIiIGZpbGw9Im5vbmUiPjwvY2lyY2xlPgo8Y2lyY2xlIGN5PSIxMDAiIHI9IjI4LjMlIiBzdHJva2Utd2lkdGg9IjEyIiBzdHJva2U9IiM5YWE2YjIiIGZpbGw9Im5vbmUiPjwvY2lyY2xlPgo8L3N2Zz4=";
        public static string verticalstripes = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI1MCIgaGVpZ2h0PSI5MCI+CjxyZWN0IHdpZHRoPSI1MCIgaGVpZ2h0PSI5MCIgZmlsbD0iZ3JleSI+PC9yZWN0Pgo8cmVjdCB4PSIyNSIgd2lkdGg9IjI1IiBoZWlnaHQ9IjkwIiBmaWxsPSIjY2NjIj48L3JlY3Q+Cjwvc3ZnPg==";
        public static string shippo = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI4MCIgaGVpZ2h0PSI4MCI+CjxyZWN0IHdpZHRoPSI4MCIgaGVpZ2h0PSI4MCIgZmlsbD0iIzliYTdiNCI+PC9yZWN0Pgo8Y2lyY2xlIGN4PSI0MCIgY3k9IjQwIiByPSI0MCIgZmlsbD0iI2RlZiI+PC9jaXJjbGU+CjxwYXRoIGQ9Ik0wIDQwIEE0MCA0MCA0NSAwIDAgNDAgMCBBNDAgNDAgMzE1IDAgMCA4MCA0MCBBNDAgNDAgNDUgMCAwIDQwIDgwIEE0MCA0MCAyNzAgMCAwIDAgNDBaIiBmaWxsPSIjOWJhN2I0Ij48L3BhdGg+Cjwvc3ZnPg==";
        public static string halfrombes = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxNSIgaGVpZ2h0PSIxNSI+CjxyZWN0IHdpZHRoPSIxNSIgaGVpZ2h0PSIxNSIgZmlsbD0iIzRmNjM4ZCI+PC9yZWN0Pgo8cGF0aCBkPSJNMCAxNUw3LjUgMEwxNSAxNVoiIGZpbGw9IiMzMDMzNTUiPjwvcGF0aD4KPC9zdmc+";
        public static string transparent = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIyMCIgaGVpZ2h0PSIyMCI+CjxyZWN0IHdpZHRoPSIyMCIgaGVpZ2h0PSIyMCIgZmlsbD0iI2ZmZiI+PC9yZWN0Pgo8cmVjdCB3aWR0aD0iMTAiIGhlaWdodD0iMTAiIGZpbGw9IiNjY2MiPjwvcmVjdD4KPHJlY3QgeD0iMTAiIHk9IjEwIiB3aWR0aD0iMTAiIGhlaWdodD0iMTAiIGZpbGw9IiNjY2MiPjwvcmVjdD4KPC9zdmc+";
        public static string simplehorizontal = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI5MCIgaGVpZ2h0PSI5Ij4KPHJlY3Qgd2lkdGg9IjkwIiBoZWlnaHQ9IjkiIGZpbGw9IiNmMmYyZjIiPjwvcmVjdD4KPHJlY3Qgd2lkdGg9IjkwIiBoZWlnaHQ9IjIiIGZpbGw9IiNlN2U3ZTciPjwvcmVjdD4KPHJlY3QgeT0iMiIgd2lkdGg9IjkwIiBoZWlnaHQ9IjMiIGZpbGw9IiNlY2VjZWMiPjwvcmVjdD4KPC9zdmc+";
        public static string whitecarbon = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIiB3aWR0aD0iNiIgaGVpZ2h0PSI2Ij4KPHJlY3Qgd2lkdGg9IjYiIGhlaWdodD0iNiIgZmlsbD0iI2VlZSI+PC9yZWN0Pgo8ZyBpZD0iYyI+CjxyZWN0IHdpZHRoPSIzIiBoZWlnaHQ9IjMiIGZpbGw9IiNlNmU2ZTYiPjwvcmVjdD4KPHJlY3QgeT0iMSIgd2lkdGg9IjMiIGhlaWdodD0iMiIgZmlsbD0iI2Q4ZDhkOCI+PC9yZWN0Pgo8L2c+Cjx1c2UgeGxpbms6aHJlZj0iI2MiIHg9IjMiIHk9IjMiPjwvdXNlPgo8L3N2Zz4=";
        public static string crossstripes = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI4IiBoZWlnaHQ9IjgiPgo8cmVjdCB3aWR0aD0iOCIgaGVpZ2h0PSI4IiBmaWxsPSIjNDAzYzNmIj48L3JlY3Q+CjxwYXRoIGQ9Ik0wIDBMOCA4Wk04IDBMMCA4WiIgc3Ryb2tlLXdpZHRoPSIxIiBzdHJva2U9IiMxZTI5MmQiPjwvcGF0aD4KPC9zdmc+";
        public static string subtledots = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI1IiBoZWlnaHQ9IjUiPgo8cmVjdCB3aWR0aD0iNSIgaGVpZ2h0PSI1IiBmaWxsPSIjZmZmIj48L3JlY3Q+CjxyZWN0IHdpZHRoPSIxIiBoZWlnaHQ9IjEiIGZpbGw9IiNjY2MiPjwvcmVjdD4KPC9zdmc+";
        public static string thinstripes = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI1IiBoZWlnaHQ9IjUiPgo8cmVjdCB3aWR0aD0iNSIgaGVpZ2h0PSI1IiBmaWxsPSIjOWU5ZTllIj48L3JlY3Q+CjxwYXRoIGQ9Ik0wIDVMNSAwWk02IDRMNCA2Wk0tMSAxTDEgLTFaIiBzdHJva2U9IiM4ODgiIHN0cm9rZS13aWR0aD0iMSI+PC9wYXRoPgo8L3N2Zz4=";
        public static string specklednoise = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIiB3aWR0aD0iNTAwIiBoZWlnaHQ9IjUwMCI+CjxmaWx0ZXIgaWQ9Im4iPgo8ZmVUdXJidWxlbmNlIHR5cGU9ImZyYWN0YWxOb2lzZSIgYmFzZUZyZXF1ZW5jeT0iLjciIG51bU9jdGF2ZXM9IjEwIiBzdGl0Y2hUaWxlcz0ic3RpdGNoIj48L2ZlVHVyYnVsZW5jZT4KPC9maWx0ZXI+CjxyZWN0IHdpZHRoPSI1MDAiIGhlaWdodD0iNTAwIiBmaWxsPSIjMDAwIj48L3JlY3Q+CjxyZWN0IHdpZHRoPSI1MDAiIGhlaWdodD0iNTAwIiBmaWx0ZXI9InVybCgjbikiIG9wYWNpdHk9IjAuNCI+PC9yZWN0Pgo8L3N2Zz4=";
        public static string blueprintgrid = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxMDAiIGhlaWdodD0iMTAwIj4KPHJlY3Qgd2lkdGg9IjEwMCIgaGVpZ2h0PSIxMDAiIGZpbGw9IiMyNjkiPjwvcmVjdD4KPGcgZmlsbD0iIzY0OTRiNyI+CjxyZWN0IHdpZHRoPSIxMDAiIGhlaWdodD0iMSIgeT0iMjAiPjwvcmVjdD4KPHJlY3Qgd2lkdGg9IjEwMCIgaGVpZ2h0PSIxIiB5PSI0MCI+PC9yZWN0Pgo8cmVjdCB3aWR0aD0iMTAwIiBoZWlnaHQ9IjEiIHk9IjYwIj48L3JlY3Q+CjxyZWN0IHdpZHRoPSIxMDAiIGhlaWdodD0iMSIgeT0iODAiPjwvcmVjdD4KPHJlY3Qgd2lkdGg9IjEiIGhlaWdodD0iMTAwIiB4PSIyMCI+PC9yZWN0Pgo8cmVjdCB3aWR0aD0iMSIgaGVpZ2h0PSIxMDAiIHg9IjQwIj48L3JlY3Q+CjxyZWN0IHdpZHRoPSIxIiBoZWlnaHQ9IjEwMCIgeD0iNjAiPjwvcmVjdD4KPHJlY3Qgd2lkdGg9IjEiIGhlaWdodD0iMTAwIiB4PSI4MCI+PC9yZWN0Pgo8L2c+CjxyZWN0IHdpZHRoPSIxMDAiIGhlaWdodD0iMTAwIiBmaWxsPSJub25lIiBzdHJva2Utd2lkdGg9IjIiIHN0cm9rZT0iI2ZmZiI+PC9yZWN0Pgo8L3N2Zz4=";
        public static string argyle = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI2MCIgaGVpZ2h0PSI5MCI+CjxnIHRyYW5zZm9ybT0ic2NhbGUoMSAxLjUpIj4KPHJlY3Qgd2lkdGg9Ijk5IiBoZWlnaHQ9Ijk5IiBmaWxsPSIjNmQ2OTVjIj48L3JlY3Q+CjxyZWN0IHdpZHRoPSI0Mi40MiIgaGVpZ2h0PSI0Mi40MiIgdHJhbnNmb3JtPSJ0cmFuc2xhdGUoMzAgMCkgcm90YXRlKDQ1KSIgZmlsbD0iIzYyNWY1MyI+PC9yZWN0Pgo8cmVjdCB3aWR0aD0iOTkiIGhlaWdodD0iMSIgdHJhbnNmb3JtPSJyb3RhdGUoNDUpIiBmaWxsPSIjNzE2ZjY0Ij48L3JlY3Q+CjxyZWN0IHdpZHRoPSI5OSIgaGVpZ2h0PSIxIiB0cmFuc2Zvcm09InRyYW5zbGF0ZSgwIDYwKSByb3RhdGUoLTQ1KSIgZmlsbD0iIzcxNmY2NCI+PC9yZWN0Pgo8L2c+Cjwvc3ZnPg==";



        public static readonly Color AliceBlue = Color.FromRgb(240, 248, 255);
        public static readonly Color AntiqueWhite = Color.FromRgb(250, 235, 215);
        public static readonly Color Aqua = Color.FromRgb(0, 255, 255);
        public static readonly Color Aquamarine = Color.FromRgb(127, 255, 212);
        public static readonly Color Azure = Color.FromRgb(240, 255, 255);
        public static readonly Color Beige = Color.FromRgb(245, 245, 220);
        public static readonly Color Bisque = Color.FromRgb(255, 228, 196);
        public static readonly Color Black = Color.FromRgb(0, 0, 0);
        public static readonly Color BlanchedAlmond = Color.FromRgb(255, 235, 205);
        public static readonly Color Blue = Color.FromRgb(0, 0, 255);
        public static readonly Color BlueViolet = Color.FromRgb(138, 43, 226);
        public static readonly Color Brown = Color.FromRgb(165, 42, 42);
        public static readonly Color BurlyWood = Color.FromRgb(222, 184, 135);
        public static readonly Color CadetBlue = Color.FromRgb(95, 158, 160);
        public static readonly Color Chartreuse = Color.FromRgb(127, 255, 0);
        public static readonly Color Chocolate = Color.FromRgb(210, 105, 30);
        public static readonly Color Coral = Color.FromRgb(255, 127, 80);
        public static readonly Color CornflowerBlue = Color.FromRgb(100, 149, 237);
        public static readonly Color Cornsilk = Color.FromRgb(255, 248, 220);
        public static readonly Color Crimson = Color.FromRgb(220, 20, 60);
        public static readonly Color Cyan = Color.FromRgb(0, 255, 255);
        public static readonly Color DarkBlue = Color.FromRgb(0, 0, 139);
        public static readonly Color DarkCyan = Color.FromRgb(0, 139, 139);
        public static readonly Color DarkGoldenrod = Color.FromRgb(184, 134, 11);
        public static readonly Color DarkGray = Color.FromRgb(169, 169, 169);
        public static readonly Color DarkGreen = Color.FromRgb(0, 100, 0);
        public static readonly Color DarkKhaki = Color.FromRgb(189, 183, 107);
        public static readonly Color DarkMagenta = Color.FromRgb(139, 0, 139);
        public static readonly Color DarkOliveGreen = Color.FromRgb(85, 107, 47);
        public static readonly Color DarkOrange = Color.FromRgb(255, 140, 0);
        public static readonly Color DarkOrchid = Color.FromRgb(153, 50, 204);
        public static readonly Color DarkRed = Color.FromRgb(139, 0, 0);
        public static readonly Color DarkSalmon = Color.FromRgb(233, 150, 122);
        public static readonly Color DarkSeaGreen = Color.FromRgb(143, 188, 143);
        public static readonly Color DarkSlateBlue = Color.FromRgb(72, 61, 139);
        public static readonly Color DarkSlateGray = Color.FromRgb(47, 79, 79);
        public static readonly Color DarkTurquoise = Color.FromRgb(0, 206, 209);
        public static readonly Color DarkViolet = Color.FromRgb(148, 0, 211);
        public static readonly Color DeepPink = Color.FromRgb(255, 20, 147);
        public static readonly Color DeepSkyBlue = Color.FromRgb(0, 191, 255);
        public static readonly Color DimGray = Color.FromRgb(105, 105, 105);
        public static readonly Color DodgerBlue = Color.FromRgb(30, 144, 255);
        public static readonly Color Firebrick = Color.FromRgb(178, 34, 34);
        public static readonly Color FloralWhite = Color.FromRgb(255, 250, 240);
        public static readonly Color ForestGreen = Color.FromRgb(34, 139, 34);
        public static readonly Color Fuschia = Color.FromRgb(255, 0, 255);
        public static readonly Color Gainsboro = Color.FromRgb(220, 220, 220);
        public static readonly Color GhostWhite = Color.FromRgb(248, 248, 255);
        public static readonly Color Gold = Color.FromRgb(255, 215, 0);
        public static readonly Color Goldenrod = Color.FromRgb(218, 165, 32);
        public static readonly Color Gray = Color.FromRgb(128, 128, 128);
        public static readonly Color Green = Color.FromRgb(0, 128, 0);
        public static readonly Color GreenYellow = Color.FromRgb(173, 255, 47);
        public static readonly Color Honeydew = Color.FromRgb(240, 255, 240);
        public static readonly Color HotPink = Color.FromRgb(255, 105, 180);
        public static readonly Color IndianRed = Color.FromRgb(205, 92, 92);
        public static readonly Color Indigo = Color.FromRgb(75, 0, 130);
        public static readonly Color Ivory = Color.FromRgb(255, 255, 240);
        public static readonly Color Khaki = Color.FromRgb(240, 230, 140);
        public static readonly Color Lavender = Color.FromRgb(230, 230, 250);
        public static readonly Color LavenderBlush = Color.FromRgb(255, 240, 245);
        public static readonly Color LawnGreen = Color.FromRgb(124, 252, 0);
        public static readonly Color LemonChiffon = Color.FromRgb(255, 250, 205);
        public static readonly Color LightBlue = Color.FromRgb(173, 216, 230);
        public static readonly Color LightCoral = Color.FromRgb(240, 128, 128);
        public static readonly Color LightCyan = Color.FromRgb(224, 255, 255);
        public static readonly Color LightGoldenrodYellow = Color.FromRgb(250, 250, 210);
        public static readonly Color LightGray = Color.FromRgb(211, 211, 211);
        public static readonly Color LightGreen = Color.FromRgb(144, 238, 144);
        public static readonly Color LightPink = Color.FromRgb(255, 182, 193);
        public static readonly Color LightSalmon = Color.FromRgb(255, 160, 122);
        public static readonly Color LightSeaGreen = Color.FromRgb(32, 178, 170);
        public static readonly Color LightSkyBlue = Color.FromRgb(135, 206, 250);
        public static readonly Color LightSlateGray = Color.FromRgb(119, 136, 153);
        public static readonly Color LightSteelBlue = Color.FromRgb(176, 196, 222);
        public static readonly Color LightYellow = Color.FromRgb(255, 255, 224);
        public static readonly Color Lime = Color.FromRgb(0, 255, 0);
        public static readonly Color LimeGreen = Color.FromRgb(50, 205, 50);
        public static readonly Color Linen = Color.FromRgb(250, 240, 230);
        public static readonly Color Magenta = Color.FromRgb(255, 0, 255);
        public static readonly Color Maroon = Color.FromRgb(128, 0, 0);
        public static readonly Color MediumAquamarine = Color.FromRgb(102, 205, 170);
        public static readonly Color MediumBlue = Color.FromRgb(0, 0, 205);
        public static readonly Color MediumOrchid = Color.FromRgb(186, 85, 211);
        public static readonly Color MediumPurple = Color.FromRgb(147, 112, 219);
        public static readonly Color MediumSeaGreen = Color.FromRgb(60, 179, 113);
        public static readonly Color MediumSlateBlue = Color.FromRgb(123, 104, 238);
        public static readonly Color MediumSpringGreen = Color.FromRgb(0, 250, 154);
        public static readonly Color MediumTurquoise = Color.FromRgb(72, 209, 204);
        public static readonly Color MediumVioletRed = Color.FromRgb(199, 21, 133);
        public static readonly Color MidnightBlue = Color.FromRgb(25, 25, 112);
        public static readonly Color MintCream = Color.FromRgb(245, 255, 250);
        public static readonly Color MistyRose = Color.FromRgb(255, 228, 225);
        public static readonly Color Moccasin = Color.FromRgb(255, 228, 181);
        public static readonly Color NavajoWhite = Color.FromRgb(255, 222, 173);
        public static readonly Color Navy = Color.FromRgb(0, 0, 128);
        public static readonly Color OldLace = Color.FromRgb(253, 245, 230);
        public static readonly Color Olive = Color.FromRgb(128, 128, 0);
        public static readonly Color OliveDrab = Color.FromRgb(107, 142, 35);
        public static readonly Color Orange = Color.FromRgb(255, 165, 0);
        public static readonly Color OrangeRed = Color.FromRgb(255, 69, 0);
        public static readonly Color Orchid = Color.FromRgb(218, 112, 214);
        public static readonly Color PaleGoldenrod = Color.FromRgb(238, 232, 170);
        public static readonly Color PaleGreen = Color.FromRgb(152, 251, 152);
        public static readonly Color PaleTurquoise = Color.FromRgb(175, 238, 238);
        public static readonly Color PaleVioletRed = Color.FromRgb(219, 112, 147);
        public static readonly Color PapayaWhip = Color.FromRgb(255, 239, 213);
        public static readonly Color PeachPuff = Color.FromRgb(255, 218, 185);
        public static readonly Color Peru = Color.FromRgb(205, 133, 63);
        public static readonly Color Pink = Color.FromRgb(255, 192, 203);
        public static readonly Color Plum = Color.FromRgb(221, 160, 221);
        public static readonly Color PowderBlue = Color.FromRgb(176, 224, 230);
        public static readonly Color Purple = Color.FromRgb(128, 0, 128);
        public static readonly Color Red = Color.FromRgb(255, 0, 0);
        public static readonly Color RosyBrown = Color.FromRgb(188, 143, 143);
        public static readonly Color RoyalBlue = Color.FromRgb(65, 105, 225);
        public static readonly Color SaddleBrown = Color.FromRgb(139, 69, 19);
        public static readonly Color Salmon = Color.FromRgb(250, 128, 114);
        public static readonly Color SandyBrown = Color.FromRgb(244, 164, 96);
        public static readonly Color SeaGreen = Color.FromRgb(46, 139, 87);
        public static readonly Color SeaShell = Color.FromRgb(255, 245, 238);
        public static readonly Color Sienna = Color.FromRgb(160, 82, 45);
        public static readonly Color Silver = Color.FromRgb(192, 192, 192);
        public static readonly Color SkyBlue = Color.FromRgb(135, 206, 235);
        public static readonly Color SlateBlue = Color.FromRgb(106, 90, 205);
        public static readonly Color SlateGray = Color.FromRgb(112, 128, 144);
        public static readonly Color Snow = Color.FromRgb(255, 250, 250);
        public static readonly Color SpringGreen = Color.FromRgb(0, 255, 127);
        public static readonly Color SteelBlue = Color.FromRgb(70, 130, 180);
        public static readonly Color Tan = Color.FromRgb(210, 180, 140);
        public static readonly Color Teal = Color.FromRgb(0, 128, 128);
        public static readonly Color Thistle = Color.FromRgb(216, 191, 216);
        public static readonly Color Tomato = Color.FromRgb(255, 99, 71);
        public static readonly Color Transparent = Color.FromRgba(255, 255, 255, 0);
        public static readonly Color Turquoise = Color.FromRgb(64, 224, 208);
        public static readonly Color Violet = Color.FromRgb(238, 130, 238);
        public static readonly Color Wheat = Color.FromRgb(245, 222, 179);
        public static readonly Color White = Color.FromRgb(255, 255, 255);
        public static readonly Color WhiteSmoke = Color.FromRgb(245, 245, 245);
        public static readonly Color Yellow = Color.FromRgb(255, 255, 0);
        public static readonly Color YellowGreen = Color.FromRgb(154, 205, 50);

        static List<Color> colorList = new List<Color>
{
AliceBlue,
AntiqueWhite,
Aqua,
Aquamarine,
Azure,
Beige,
Bisque,
Black,
BlanchedAlmond,
Blue,
BlueViolet,
Brown,
BurlyWood,
CadetBlue,
Chartreuse,
Chocolate,
Coral,
CornflowerBlue,
Cornsilk,
Crimson,
Cyan,
DarkBlue,
DarkCyan,
DarkGoldenrod,
DarkGray,
DarkGreen,
DarkKhaki,
DarkMagenta,
DarkOliveGreen,
DarkOrange,
DarkOrchid,
DarkRed,
DarkSalmon,
DarkSeaGreen,
DarkSlateBlue,
DarkSlateGray,
DarkTurquoise,
DarkViolet,
DeepPink,
DeepSkyBlue,
DimGray,
DodgerBlue,
Firebrick,
FloralWhite,
ForestGreen,
Fuschia,
Gainsboro,
GhostWhite,
Gold,
Goldenrod,
Gray,
Green,
GreenYellow,
Honeydew,
HotPink,
IndianRed,
Indigo,
Ivory,
Khaki,
Lavender,
LavenderBlush,
LawnGreen,
LemonChiffon,
LightBlue,
LightCoral,
LightCyan,
LightGoldenrodYellow,
LightGray,
LightGreen,
LightPink,
LightSalmon,
LightSeaGreen,
LightSkyBlue,
LightSlateGray,
LightSteelBlue,
LightYellow,
Lime,
LimeGreen,
Linen,
Magenta,
Maroon,
MediumAquamarine,
MediumBlue,
MediumOrchid,
MediumPurple,
MediumSeaGreen,
MediumSlateBlue,
MediumSpringGreen,
MediumTurquoise,
MediumVioletRed,
MidnightBlue,
MintCream,
MistyRose,
Moccasin,
NavajoWhite,
Navy,
OldLace,
Olive,
OliveDrab,
Orange,
OrangeRed,
Orchid,
PaleGoldenrod,
PaleGreen,
PaleTurquoise,
PaleVioletRed,
PapayaWhip,
PeachPuff,
Peru,
Pink,
Plum,
PowderBlue,
Purple,
Red,
RosyBrown,
RoyalBlue,
SaddleBrown,
Salmon,
SandyBrown,
SeaGreen,
SeaShell,
Sienna,
Silver,
SkyBlue,
SlateBlue,
SlateGray,
Snow,
SpringGreen,
SteelBlue,
Tan,
Teal,
Thistle,
Tomato,
Transparent,
Turquoise,
Violet,
Wheat,
White,
WhiteSmoke,
Yellow,
YellowGreen
};

    }
}