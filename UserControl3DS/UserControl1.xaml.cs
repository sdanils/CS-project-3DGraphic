using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace UserControl3DSecond
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserViewport3D : UserControl
    {
        //
        DispatcherTimer timer;
        //Статус анимации
        bool animOn;
        PerspectiveCamera myCamera3D;
        System.Windows.Point lastPositionMouse;
        //Краска графика
        LinearGradientBrush BrushFunction;

        //Обработчики событий
        private void viewport3D_MouseDownSec(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Сохраняем позиции курсора, если кнопка нажата
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point mouseposition = e.GetPosition(viewport3DSecond);
                Point3D testpoint3D = new Point3D(mouseposition.X, mouseposition.Y, 0);
                Vector3D testdirection = new Vector3D(mouseposition.X, mouseposition.Y, 10);
                PointHitTestParameters pointparams = new PointHitTestParameters(mouseposition);

                //test for a result in the Viewport3D
                VisualTreeHelper.HitTest(viewport3DSecond, null, HTResult, pointparams);
            }
        }
        private void viewportt3D_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                // Вычисление смещения курсора
                int offsetX = (int)e.GetPosition(viewport3DSecond).X - (int)lastPositionMouse.X;
                int offsetY = (int)e.GetPosition(viewport3DSecond).Y - (int)lastPositionMouse.Y;

                // Вызов метода с величиной смещения
                RotateCamera(offsetX, offsetY);

                // Обновление последней позиции курсора
                lastPositionMouse = e.GetPosition(viewport3DSecond);
            }
        }
        private void viewoprt3D_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            int numberOfTextLinesToMove = e.Delta*25;
            this.ChangeCameraDistance(-0.001 * numberOfTextLinesToMove);
        }
        private void viewport3D_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                lastPositionMouse = e.GetPosition(viewport3DSecond);
            }
        }
        public UserViewport3D(Point3D[] points, int rows, int columns)
        {
            InitializeComponent();

            animOn = false;
            //Создаем камеру для отображения моделей
            myCamera3D = new PerspectiveCamera();
            myCamera3D.Position = new Point3D(12, 24, 32);
            myCamera3D.LookDirection = new Vector3D(-1, -2, -3);
            myCamera3D.FieldOfView = 45;
            viewport3DSecond.Camera = myCamera3D;

            //Обьект трансформаций для камеры, для поворота
            Transform3DGroup transformGroup = new Transform3DGroup();
            myCamera3D.Transform = transformGroup;

            //Обьект представляющий группу рисуемых обьектов
            Model3DGroup modelGroup = new Model3DGroup();

            //Подписываем на события нажатия и передвижение курсора
            viewport3DSecond.MouseDown += viewport3D_MouseDown;
            viewport3DSecond.MouseMove += viewportt3D_MouseMove;
            viewport3DSecond.MouseWheel += viewoprt3D_MouseWheel;
            viewport3DSecond.MouseDown += viewport3D_MouseDownSec;

            //Создание оси Х
            MeshGeometry3D meshX = new MeshGeometry3D();
            Point3D[] pointsLines = new Point3D[] {
                new Point3D(10,0.01,0.01),
                new Point3D(10,-0.01,0.01),
                new Point3D(10,0.01,-0.01),
                new Point3D(10,-0.01,-0.01),
                new Point3D(-10,0.01,0.01),
                new Point3D(-10,-0.01,0.01),
                new Point3D(-10,0.01,-0.01),
                new Point3D(-10,-0.01,-0.01),
            };
            meshX.Positions = new Point3DCollection(pointsLines);
            meshX.TriangleIndices = new Int32Collection(new int[] { 0, 1, 2, 2, 3, 1, 1, 5, 4, 4, 1, 0, 0, 4, 6, 6, 0, 2, 2, 6, 7, 7, 2, 3, 3, 1, 5, 5, 3, 7, 7, 5, 4, 4, 7, 6 });

            GeometryModel3D linesX = new GeometryModel3D();
            linesX.Geometry = meshX;
            linesX.Material = new DiffuseMaterial(System.Windows.Media.Brushes.Red);
            linesX.BackMaterial = new DiffuseMaterial(System.Windows.Media.Brushes.Red);
            modelGroup.Children.Add(linesX);

            //Создание оси У
            MeshGeometry3D meshY = new MeshGeometry3D();
            pointsLines = new Point3D[] {
                new Point3D(0.01,10,0.01),
                new Point3D(-0.01,10,0.01),
                new Point3D(0.01,10,-0.01),
                new Point3D(-0.01, 10,-0.01),
                new Point3D(0.01,-10,0.01),
                new Point3D(-0.01, -10,0.01),
                new Point3D(0.01, -10,-0.01),
                new Point3D(-0.01, -10,-0.01),
            };
            meshY.Positions = new Point3DCollection(pointsLines);
            meshY.TriangleIndices = new Int32Collection(new int[] { 0, 1, 2, 2, 3, 1, 1, 5, 4, 4, 1, 0, 0, 4, 6, 6, 0, 2, 2, 6, 7, 7, 2, 3, 3, 1, 5, 5, 3, 7, 7, 5, 4, 4, 7, 6 });

            GeometryModel3D linesY = new GeometryModel3D();
            linesY.Geometry = meshY;
            linesY.Material = new DiffuseMaterial(System.Windows.Media.Brushes.Blue);
            linesY.BackMaterial = new DiffuseMaterial(System.Windows.Media.Brushes.Blue);
            modelGroup.Children.Add(linesY);

            //Создание оси Z
            MeshGeometry3D meshZ = new MeshGeometry3D();
            pointsLines = new Point3D[] {
                new Point3D(0.01,0.01,10),
                new Point3D(-0.01,0.01,10),
                new Point3D(0.01,-0.01,10),
                new Point3D(-0.01,-0.01,10),
                new Point3D(0.01,0.01,-10),
                new Point3D(-0.01,0.01,-10),
                new Point3D(0.01,-0.01,-10),
                new Point3D(-0.01,-0.01,-10),
            };
            meshZ.Positions = new Point3DCollection(pointsLines);
            meshZ.TriangleIndices = new Int32Collection(new int[] { 0, 1, 2, 2, 3, 1, 1, 5, 4, 4, 1, 0, 0, 4, 6, 6, 0, 2, 2, 6, 7, 7, 2, 3, 3, 1, 5, 5, 3, 7, 7, 5, 4, 4, 7, 6 });

            GeometryModel3D linesZ = new GeometryModel3D();
            linesZ.Geometry = meshZ;
            linesZ.Material = new DiffuseMaterial(System.Windows.Media.Brushes.Green);
            linesZ.BackMaterial = new DiffuseMaterial(System.Windows.Media.Brushes.Green);
            modelGroup.Children.Add(linesZ);

            //Сохдаем поверхность
            MeshGeometry3D meshFunction = new MeshGeometry3D();
            for (int i = 0; i < points.Length; i++)
            {
                meshFunction.Positions.Add(points[i]);
                meshFunction.TextureCoordinates.Add(new System.Windows.Point(0, points[i].Y));
            }

            // Генерация индексов для треугольников
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < columns - 1; j++)
                {
                    int currentIndex = i * columns + j;

                    // Добавление треугольников для текущего квадрата
                    meshFunction.TriangleIndices.Add(currentIndex);
                    meshFunction.TriangleIndices.Add(currentIndex + 1);
                    meshFunction.TriangleIndices.Add(currentIndex + columns);

                    meshFunction.TriangleIndices.Add(currentIndex + 1);
                    meshFunction.TriangleIndices.Add(currentIndex + columns + 1);
                    meshFunction.TriangleIndices.Add(currentIndex + columns);
                }
            }
            GeometryModel3D function = new GeometryModel3D();
            function.Geometry = meshFunction;

            LinearGradientBrush materialGrad = new LinearGradientBrush();
            materialGrad.StartPoint = new System.Windows.Point(0.5, 0);
            materialGrad.EndPoint = new System.Windows.Point(0.5, 1);

            materialGrad.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(230, 0, 0, 255), 0));
            materialGrad.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(230, 0, 128, 0), 0.25));
            materialGrad.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(230, 255, 255, 0), 0.5));
            materialGrad.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(230, 255, 165, 0), 0.75));
            materialGrad.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(230, 255, 0, 0), 1));

            //Сохраняем краску
            BrushFunction = materialGrad;

            function.Material = new DiffuseMaterial(materialGrad);
            function.BackMaterial = new DiffuseMaterial(materialGrad);
            modelGroup.Children.Add(function);

            //Невидимые квадраты, для удобного отслеживания наведения
            Point3D[] pointsQuads = new Point3D[]
            {
            new Point3D(20, -20, 0),
            new Point3D(20, 20, 0),
            new Point3D(-20, 20, 0),
            new Point3D(-20, -20, 0),

            new Point3D(20, 0, -20),
            new Point3D(20, 0, 20),
            new Point3D(-20, 0, 20),
            new Point3D(-20, 0, -20),

            new Point3D(0, 20, -20),
            new Point3D(0, 20, 20),
            new Point3D(0, -20, 20),
            new Point3D(0, -20, -20),
            };
            MeshGeometry3D meshGeometry = new MeshGeometry3D();

            meshGeometry.Positions = new Point3DCollection(pointsQuads);
            meshGeometry.TriangleIndices = new Int32Collection(new int[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8 });

            System.Windows.Media.Color surfaceColor = Colors.White;
            surfaceColor.A = 0;
            Material surfaceMaterial = new DiffuseMaterial(new SolidColorBrush(surfaceColor));

            GeometryModel3D modelQuads = new GeometryModel3D();
            modelQuads.Geometry = meshGeometry;
            modelQuads.Material = surfaceMaterial;
            modelQuads.BackMaterial = surfaceMaterial;
            modelGroup.Children.Add(modelQuads);

            //Создание источников освещения
            DirectionalLight myLight = new DirectionalLight(System.Windows.Media.Colors.White, new Vector3D(0, -1, 0));
            modelGroup.Children.Add(myLight);
            DirectionalLight myLight2 = new DirectionalLight(System.Windows.Media.Colors.White, new Vector3D(0, 1, 0));
            modelGroup.Children.Add(myLight2);
            DirectionalLight myLight3 = new DirectionalLight(System.Windows.Media.Colors.White, new Vector3D(1, 0, 0));
            modelGroup.Children.Add(myLight3);
            DirectionalLight myLight4 = new DirectionalLight(System.Windows.Media.Colors.White, new Vector3D(-1, 0, 0));
            modelGroup.Children.Add(myLight4);
            DirectionalLight myLight5 = new DirectionalLight(System.Windows.Media.Colors.White, new Vector3D(0, 0, 1));
            modelGroup.Children.Add(myLight5);
            DirectionalLight myLight6 = new DirectionalLight(System.Windows.Media.Colors.White, new Vector3D(0, 0, -1));
            modelGroup.Children.Add(myLight6);

            ModelVisual3D modelVisual = new ModelVisual3D();
            modelVisual.Content = modelGroup;

            viewport3DSecond.Children.Add(modelVisual);

            //Таймер для анимации
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10); // Интервал в миллисекундах (в данном случае, 1 секунда)
            timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Установка нового смещения
            BrushFunction.GradientStops[0].Offset += 0.01;
            BrushFunction.GradientStops[1].Offset += 0.01;
            BrushFunction.GradientStops[2].Offset += 0.01;
            BrushFunction.GradientStops[3].Offset += 0.01;
            BrushFunction.GradientStops[4].Offset += 0.01;

            //Проверка на повтор цвета
            for (int i = 0; i < 5; ++i)
            {
                if (BrushFunction.GradientStops[i].Offset > 1.05)
                {
                    BrushFunction.GradientStops[i].Offset = -0.05;
                    break;
                }
            }
        }
        //Перемещение камеры реализующее приближение
        public void ChangeCameraDistance(double dDistance)
        {/*Изменяем размер радиус вектора камеры, которая направлена всегда в центр,
          с использованием трехмерных полярных координат*/

            //считаем радиус до камеры
            double rho = Math.Sqrt(
                myCamera3D.Position.X * myCamera3D.Position.X +
                myCamera3D.Position.Y * myCamera3D.Position.Y +
                myCamera3D.Position.Z * myCamera3D.Position.Z);

            //проверка возмодность изменения растояния
            if (dDistance < 0 && rho < 3)
            {
                return;
            }
            else if (dDistance > 0 && rho > 50)
            {
                return;
            }

            //ищем координаты углов
            double phi = Math.Acos(myCamera3D.Position.Z / rho);
            double theta = Math.Atan2(myCamera3D.Position.Y, myCamera3D.Position.X);

            // Меняем значение радиуса (расстояния до центра)
            if (rho > 20)
            {
                rho += dDistance * 2;
            }
            else if (rho < 20)
            {
                rho += dDistance;
            }

            // Пересчитываем координаты обратно в декартовы
            double x = rho * Math.Sin(phi) * Math.Cos(theta);
            double y = rho * Math.Sin(phi) * Math.Sin(theta);
            double z = rho * Math.Cos(phi);

            // Устанавливаем новую позицию камеры
            myCamera3D.Position = new Point3D(x, y, z);
            ButtonAnimation.Width = this.Width / 20;
            ButtonAnimation.Height = this.Height / 20;
            
        }
        //Вражение камеры
        public void RotateCamera(double dX, double dY)
        {
            RotateTransform3D myRotateTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), -dX / 8));
            RotateTransform3D myRotateTransformTwo = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), -dY / 8));

            ((Transform3DGroup)myCamera3D.Transform).Children.Add(myRotateTransform);
            ((Transform3DGroup)myCamera3D.Transform).Children.Add(myRotateTransformTwo);
            myCamera3D.UpDirection = new Vector3D(0, 1, 0);
        }
        private void ButtonAnimation_Click(object sender, RoutedEventArgs e)
        {
            if (!animOn)
            {
                timer.Start();
                animOn = true;
            }
            else
            {
                timer.Stop();
                animOn = false;
            }
        }

        public HitTestResultBehavior HTResult(System.Windows.Media.HitTestResult rawresult)
        {
            RayMeshGeometry3DHitTestResult rayHTResult = rawresult as RayMeshGeometry3DHitTestResult;
            if (rayHTResult != null)
            {
                Point3D point3D = rayHTResult.PointHit;

                ModelVisual3D modelVisual3D = viewport3DSecond.Children[0] as ModelVisual3D;
                Model3DGroup modelGroup = (Model3DGroup)(modelVisual3D.Content);
                GeometryModel3D graphModel = (GeometryModel3D)modelGroup.Children[3];
                MeshGeometry3D graphMesh = (MeshGeometry3D)graphModel.Geometry;

                Point3D[] positions = graphMesh.Positions.ToArray();

                for (int i = 0; i < positions.Length; i++)
                {
                    double distanceToHitPoint = (positions[i] - point3D).Length;

                    if (distanceToHitPoint < 1)
                    {
                        // Модификация высоты вершины для создания "ямы"
                        positions[i] = new Point3D(positions[i].X, positions[i].Y - (1 - distanceToHitPoint * distanceToHitPoint), positions[i].Z);
                    }

                }

                // Обновляем координаты вершин
                graphMesh.Positions = new Point3DCollection(positions);

                viewport3DSecond.Children.Clear();

                //Обьект представляющий группу рисуемых обьектов
                modelGroup = new Model3DGroup();

                MeshGeometry3D meshX = new MeshGeometry3D();
                Point3D[] pointsLines = new Point3D[] {
                new Point3D(10,0.01,0.01),
                new Point3D(10,-0.01,0.01),
                new Point3D(10,0.01,-0.01),
                new Point3D(10,-0.01,-0.01),
                new Point3D(-10,0.01,0.01),
                new Point3D(-10,-0.01,0.01),
                new Point3D(-10,0.01,-0.01),
                new Point3D(-10,-0.01,-0.01),
            };
                meshX.Positions = new Point3DCollection(pointsLines);
                meshX.TriangleIndices = new Int32Collection(new int[] { 0, 1, 2, 2, 3, 1, 1, 5, 4, 4, 1, 0, 0, 4, 6, 6, 0, 2, 2, 6, 7, 7, 2, 3, 3, 1, 5, 5, 3, 7, 7, 5, 4, 4, 7, 6 });

                GeometryModel3D linesX = new GeometryModel3D();
                linesX.Geometry = meshX;
                linesX.Material = new DiffuseMaterial(System.Windows.Media.Brushes.Red);
                linesX.BackMaterial = new DiffuseMaterial(System.Windows.Media.Brushes.Red);
                modelGroup.Children.Add(linesX);

                //Создание оси У
                MeshGeometry3D meshY = new MeshGeometry3D();
                pointsLines = new Point3D[] {
                new Point3D(0.01,10,0.01),
                new Point3D(-0.01,10,0.01),
                new Point3D(0.01,10,-0.01),
                new Point3D(-0.01, 10,-0.01),
                new Point3D(0.01,-10,0.01),
                new Point3D(-0.01, -10,0.01),
                new Point3D(0.01, -10,-0.01),
                new Point3D(-0.01, -10,-0.01),
            };
                meshY.Positions = new Point3DCollection(pointsLines);
                meshY.TriangleIndices = new Int32Collection(new int[] { 0, 1, 2, 2, 3, 1, 1, 5, 4, 4, 1, 0, 0, 4, 6, 6, 0, 2, 2, 6, 7, 7, 2, 3, 3, 1, 5, 5, 3, 7, 7, 5, 4, 4, 7, 6 });

                GeometryModel3D linesY = new GeometryModel3D();
                linesY.Geometry = meshY;
                linesY.Material = new DiffuseMaterial(System.Windows.Media.Brushes.Blue);
                linesY.BackMaterial = new DiffuseMaterial(System.Windows.Media.Brushes.Blue);
                modelGroup.Children.Add(linesY);

                //Создание оси Z
                MeshGeometry3D meshZ = new MeshGeometry3D();
                pointsLines = new Point3D[] {
                new Point3D(0.01,0.01,10),
                new Point3D(-0.01,0.01,10),
                new Point3D(0.01,-0.01,10),
                new Point3D(-0.01,-0.01,10),
                new Point3D(0.01,0.01,-10),
                new Point3D(-0.01,0.01,-10),
                new Point3D(0.01,-0.01,-10),
                new Point3D(-0.01,-0.01,-10),
            };
                meshZ.Positions = new Point3DCollection(pointsLines);
                meshZ.TriangleIndices = new Int32Collection(new int[] { 0, 1, 2, 2, 3, 1, 1, 5, 4, 4, 1, 0, 0, 4, 6, 6, 0, 2, 2, 6, 7, 7, 2, 3, 3, 1, 5, 5, 3, 7, 7, 5, 4, 4, 7, 6 });

                GeometryModel3D linesZ = new GeometryModel3D();
                linesZ.Geometry = meshZ;
                linesZ.Material = new DiffuseMaterial(System.Windows.Media.Brushes.Green);
                linesZ.BackMaterial = new DiffuseMaterial(System.Windows.Media.Brushes.Green);
                modelGroup.Children.Add(linesZ);


                GeometryModel3D function = new GeometryModel3D();
                function.Geometry = graphMesh;

                LinearGradientBrush materialGrad = new LinearGradientBrush();
                materialGrad.StartPoint = new System.Windows.Point(0.5, 0);
                materialGrad.EndPoint = new System.Windows.Point(0.5, 1);

                materialGrad.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(230, 0, 0, 255), 0));
                materialGrad.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(230, 0, 128, 0), 0.25));
                materialGrad.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(230, 255, 255, 0), 0.5));
                materialGrad.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(230, 255, 165, 0), 0.75));
                materialGrad.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(230, 255, 0, 0), 1));

                //Сохраняем краску
                BrushFunction = materialGrad;

                function.Material = new DiffuseMaterial(materialGrad);
                function.BackMaterial = new DiffuseMaterial(materialGrad);;
                modelGroup.Children.Add(function);

                //Невидимые квадраты, для удобного отслеживания наведения
                Point3D[] pointsQuads = new Point3D[]
                {
            new Point3D(20, -20, 0),
            new Point3D(20, 20, 0),
            new Point3D(-20, 20, 0),
            new Point3D(-20, -20, 0),

            new Point3D(20, 0, -20),
            new Point3D(20, 0, 20),
            new Point3D(-20, 0, 20),
            new Point3D(-20, 0, -20),

            new Point3D(0, 20, -20),
            new Point3D(0, 20, 20),
            new Point3D(0, -20, 20),
            new Point3D(0, -20, -20),
                };
                MeshGeometry3D meshGeometry = new MeshGeometry3D();

                meshGeometry.Positions = new Point3DCollection(pointsQuads);
                meshGeometry.TriangleIndices = new Int32Collection(new int[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8 });

                System.Windows.Media.Color surfaceColor = Colors.White;
                surfaceColor.A = 0;
                Material surfaceMaterial = new DiffuseMaterial(new SolidColorBrush(surfaceColor));

                GeometryModel3D modelQuads = new GeometryModel3D();
                modelQuads.Geometry = meshGeometry;
                modelQuads.Material = surfaceMaterial;
                modelQuads.BackMaterial = surfaceMaterial;
                modelGroup.Children.Add(modelQuads);

                //Создание источников освещения
                DirectionalLight myLight = new DirectionalLight(System.Windows.Media.Colors.White, new Vector3D(0, -1, 0));
                modelGroup.Children.Add(myLight);
                DirectionalLight myLight2 = new DirectionalLight(System.Windows.Media.Colors.White, new Vector3D(0, 1, 0));
                modelGroup.Children.Add(myLight2);
                DirectionalLight myLight3 = new DirectionalLight(System.Windows.Media.Colors.White, new Vector3D(1, 0, 0));
                modelGroup.Children.Add(myLight3);
                DirectionalLight myLight4 = new DirectionalLight(System.Windows.Media.Colors.White, new Vector3D(-1, 0, 0));
                modelGroup.Children.Add(myLight4);
                DirectionalLight myLight5 = new DirectionalLight(System.Windows.Media.Colors.White, new Vector3D(0, 0, 1));
                modelGroup.Children.Add(myLight5);
                DirectionalLight myLight6 = new DirectionalLight(System.Windows.Media.Colors.White, new Vector3D(0, 0, -1));
                modelGroup.Children.Add(myLight6);

                ModelVisual3D modelVisual = new ModelVisual3D();
                modelVisual.Content = modelGroup;

                viewport3DSecond.Children.Add(modelVisual);
                
            }

            return HitTestResultBehavior.Stop;
        }
    }
}
