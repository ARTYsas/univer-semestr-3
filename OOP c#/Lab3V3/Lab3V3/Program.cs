using System.Xml;
using System.Xml.Linq;

// Base class for shapes
abstract class Shape
{
    public double frameThickness;

    public Shape(double frameThickness)
    {
        this.frameThickness = frameThickness;
    }

    public abstract double CalculateArea();
    public abstract void DrawAsSymbols();

    internal void SetArea(double area)
    {
        CalculateArea();
    }
}



// Circle shape
class Circle : Shape
{
    private double radius;

    public Circle(double radius, double frameThickness) : base(frameThickness)
    {
        this.radius = radius;
    }

    public Circle(double frameThickness) : base(frameThickness)
    {

    }

    public override double CalculateArea()
    {
        return Math.PI * Math.Pow(radius, 2);
    }

    public override void DrawAsSymbols()
    {
        int diameter = (int)radius * 2;
        int centerX = (int)radius;
        int centerY = (int)radius;

        for (int y = 0; y <= diameter; y++)
        {
            for (int x = 0; x <= diameter; x++)
            {
                int distance = (x - centerX) * (x - centerX) + (y - centerY) * (y - centerY);
                int squaredRadius = (int)(radius * radius);

                if (distance >= squaredRadius - radius && distance <= squaredRadius + radius)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }

}

// Square shape
class Square : Shape
{
    private double sideLength;

    public Square(double sideLength, double frameThickness) : base(frameThickness)
    {
        this.sideLength = sideLength;
    }

    public Square(double frameThickness) : base(frameThickness)
    {

    }

    public override double CalculateArea()
    {
        return Math.Pow(sideLength, 2);
    }

    public override void DrawAsSymbols()
    {
        for (int i = 0; i < sideLength; i++)
        {
            for (int j = 0; j < sideLength; j++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }
    }
}

// Ellipse shape
class Ellipse : Shape
{
    private double semiMajorAxis;
    private double semiMinorAxis;

    public Ellipse(double semiMajorAxis, double semiMinorAxis, double frameThickness) : base(frameThickness)
    {
        this.semiMajorAxis = semiMajorAxis;
        this.semiMinorAxis = semiMinorAxis;
    }

    public Ellipse(double frameThickness) : base(frameThickness)
    {

    }

    public override double CalculateArea()
    {
        return Math.PI * semiMajorAxis * semiMinorAxis;
    }

    public override void DrawAsSymbols()
    {
        int width = (int)(2 * semiMajorAxis);
        int height = (int)(2 * semiMajorAxis);

        for (int y = -width; y <= width; y++)
        {
            for (int x = -height; x <= height; x++)
            {
                double normalizedX = (double)x / semiMajorAxis;
                double normalizedY = (double)y / semiMinorAxis;
                double distance = Math.Pow(normalizedX, 2) + Math.Pow(normalizedY, 2);

                if (distance <= 1)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.WriteLine();
        }
    }
}

// Rectangle shape
class Rectangle : Shape
{
    private double width;
    private double height;

    public Rectangle(double width, double height, double frameThickness) : base(frameThickness)
    {
        this.width = width;
        this.height = height;
    }

    public Rectangle(double frameThickness) : base(frameThickness)
    {
  
    }

    public override double CalculateArea()
    {
        return width * height;
    }

    public override void DrawAsSymbols()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }
    }
}

// Graphical editor class
class GraphicalEditor
{
    private List<Shape> shapes;
    private double averageArea;

    public GraphicalEditor()
    {
        shapes = new List<Shape>();
        averageArea = 0.0;
    }

    public void AddShape(Shape shape)
    {
        shapes.Add(shape);
        UpdateAverageArea();
    }

    public void CalculateAreas()
    {
        foreach (Shape shape in shapes)
        {
            double area = shape.CalculateArea();
            Console.WriteLine($"Shape: {shape.GetType().Name}, Area: {area}");
        }
    }

    public void ArrangeFigures()
    {
        var sortedFigures = shapes.OrderBy(shape => shape.CalculateArea())
                                  .ThenBy(shape => shape.frameThickness);

        Console.WriteLine("Arranged Figures:");
        foreach (var figure in sortedFigures)
        {
            Console.WriteLine($"Type: {figure.GetType().Name}, Frame Thickness: {figure.frameThickness}, Area: {figure.CalculateArea()}");
        }
    }

    public void OutputFirstThreeShapes()
    {
        var firstThreeShapes = shapes.Take(3);

        Console.WriteLine("First Three Shapes:");
        foreach (var shape in firstThreeShapes)
        {
            Console.WriteLine($"Type: {shape.GetType().Name}, Frame Thickness: {shape.frameThickness}, Area: {shape.CalculateArea()}");
        }
    }

    public void DrawLastTwoFiguresAsSymbols()
    {
        Console.WriteLine("Drawing Last Two Figures as Symbols ('*'):");

        var lastTwoFigures = shapes.Skip(Math.Max(0, shapes.Count - 2));

        foreach (var figure in lastTwoFigures)
        {
            Console.WriteLine($"Type: {figure.GetType().Name}, Frame Thickness: {figure.frameThickness}, Area: {figure.CalculateArea()}");

            Console.WriteLine("Symbol representation:");
            figure.DrawAsSymbols();
            Console.WriteLine();
        }
    }

    private void UpdateAverageArea()
    {
        double totalArea = 0.0;

        foreach (Shape shape in shapes)
        {
            totalArea += shape.CalculateArea();
        }

        averageArea = totalArea / shapes.Count;
    }

    public void OutputAverageArea()
    {
        Console.WriteLine($"Average Area: {averageArea}");
    }

    public void WriteDataToXmlFile(string filename)
    {
        XDocument doc = new XDocument();

        XElement root = new XElement("GraphicalEditor");

        foreach (Shape shape in shapes)
        {
            XElement shapeElement = new XElement("Shape",
                new XAttribute("Type", shape.GetType().Name),
                new XAttribute("FrameThickness", shape.frameThickness),
                new XAttribute("Area", shape.CalculateArea()));

            root.Add(shapeElement);
        }

        doc.Add(root);
        doc.Save(filename);
    }

    public void ReadDataFromXmlFile(string filename)
    {
        XDocument doc = XDocument.Load(filename);

        IEnumerable<XElement> shapeElements = doc.Descendants("Shape");

        foreach (XElement shapeElement in shapeElements)
        {
            string shapeType = shapeElement.Attribute("Type").Value;
            string frameThicknessStr = shapeElement.Attribute("FrameThickness").Value;
            string areaStr = shapeElement.Attribute("Area").Value;

            int frameThickness;
            if (!int.TryParse(frameThicknessStr, out frameThickness))
            {
                Console.WriteLine($"Invalid frame thickness value: {frameThicknessStr}. Skipping shape.");
                continue;
            }

            double area;
            if (!double.TryParse(areaStr, out area))
            {
                Console.WriteLine($"Invalid area value: {areaStr}. Skipping shape.");
                continue;
            }

            Shape shape;

            switch (shapeType)
            {
                case "Circle":
                    shape = new Circle(frameThickness);
                    break;

                case "Square":
                    shape = new Square(frameThickness);
                    break;

                case "Ellipse":
                    shape = new Ellipse(frameThickness);
                    break;

                case "Rectangle":
                    shape = new Rectangle(frameThickness);
                    break;

                default:
                    Console.WriteLine($"Invalid shape type: {shapeType}. Skipping shape.");
                    continue;
            }

            shapes.Add(shape);
            shape.SetArea(area);
        }
    }

    public void SaveDataToXml()
    {
        Console.Write("Enter the filename to save the data: ");
        string filename = Console.ReadLine();
        WriteDataToXmlFile(filename);
        Console.WriteLine("Data saved to XML file successfully.");
    }

    public void LoadDataFromXml()
    {
        Console.Write("Enter the filename to load the data from: ");
        string filename = Console.ReadLine();
        try
        {
            ReadDataFromXmlFile(filename);
            Console.WriteLine("Data loaded from XML file successfully.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found. Please enter a valid filename.");
        }
        catch (XmlException)
        {
            Console.WriteLine("Invalid XML file format. Please ensure the file is in the correct format.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading data from the XML file: {ex.Message}");
        }
    }

}

class Program
{
    static void Main(string[] args)
    {
        GraphicalEditor editor = new GraphicalEditor();

        bool exitProgram = false;

        while (!exitProgram)
        {
            Console.WriteLine("========== Graphical Editor ==========");
            Console.WriteLine("1. Add Circle");
            Console.WriteLine("2. Add Square");
            Console.WriteLine("3. Add Ellipse");
            Console.WriteLine("4. Add Rectangle");
            Console.WriteLine("5. Calculate Areas");
            Console.WriteLine("6. Exit");
            Console.WriteLine("7. Arrange Figures by Area and Frame Thickness");
            Console.WriteLine("8. Output First Three Shapes");
            Console.WriteLine("9. Draw Last Two Figures as Symbols");
            Console.WriteLine("10. output the average area of shapes");
            Console.WriteLine("13. Save Data to XML");
            Console.WriteLine("14. Load Data from XML");
            Console.WriteLine("======================================");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter radius of the circle: ");
                    double circleRadius = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter frame thickness of the circle: ");
                    double circleFrameThickness = Convert.ToDouble(Console.ReadLine());
                    Circle circle = new Circle(circleRadius, circleFrameThickness);
                    editor.AddShape(circle);
                    Console.WriteLine("Circle added.");
                    break;

                case "2":
                    Console.Write("Enter side length of the square: ");
                    double squareSideLength = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter frame thickness of the square: ");
                    double squareFrameThickness = Convert.ToDouble(Console.ReadLine());
                    Square square = new Square(squareSideLength, squareFrameThickness);
                    editor.AddShape(square);
                    Console.WriteLine("Square added.");
                    break;

                case "3":
                    Console.Write("Enter semi-major axis of the ellipse: ");
                    double ellipseSemiMajorAxis = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter semi-minor axis of the ellipse: ");
                    double ellipseSemiMinorAxis = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter frame thickness of the ellipse: ");
                    double ellipseFrameThickness = Convert.ToDouble(Console.ReadLine());
                    Ellipse ellipse = new Ellipse(ellipseSemiMajorAxis, ellipseSemiMinorAxis, ellipseFrameThickness);
                    editor.AddShape(ellipse);
                    Console.WriteLine("Ellipse added.");
                    break;

                case "4":
                    Console.Write("Enter width of the rectangle: ");
                    double rectangleWidth = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter height of the rectangle: ");
                    double rectangleHeight = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter frame thickness of the rectangle: ");
                    double rectangleFrameThickness = Convert.ToDouble(Console.ReadLine());
                    Rectangle rectangle = new Rectangle(rectangleWidth, rectangleHeight, rectangleFrameThickness);
                    editor.AddShape(rectangle);
                    Console.WriteLine("Rectangle added.");
                    break;

                case "5":
                    editor.CalculateAreas();
                    break;

                case "6":
                    exitProgram = true;
                    Console.WriteLine("Exiting the program...");
                    break;

                case "7":
                    editor.ArrangeFigures();
                    break;

                case "8":
                    editor.OutputFirstThreeShapes();
                    break;

                case "9":
                    editor.DrawLastTwoFiguresAsSymbols();
                    break;

                case "10":
                    editor.OutputAverageArea();
                    break;

                case "13":
                    editor.SaveDataToXml();
                    break;

                case "14":
                    editor.LoadDataFromXml();
                    break;


                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

