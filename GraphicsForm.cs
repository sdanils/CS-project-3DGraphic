using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using UserControl3DSecond;


namespace Lab_Four_WinAdd
{
    public partial class GraphicsForm : Form
    { 
        public GraphicsForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int rows = 100;
            int columns = rows;

            CreaterFunctions creater = new CreaterFunctions();
            
            //Создание первой вкладки
            ElementHost elementHost3DOne = new ElementHost();
            elementHost3DOne.Dock = DockStyle.Fill;
            Point3D[] arrayCoord = creater.CreatedArrayMeans(1);
            UserViewport3D viewport3DOne = new UserViewport3D(arrayCoord, rows, columns);
            elementHost3DOne.Child = viewport3DOne;
            PageOne.Controls.Add(elementHost3DOne);

            //Создание второй вкладки
            ElementHost elementHost3DTwo = new ElementHost();
            elementHost3DTwo.Dock = DockStyle.Fill;
            arrayCoord = creater.CreatedArrayMeans(2);
            UserViewport3D viewport3DTwo = new UserViewport3D(arrayCoord, rows, columns);
            elementHost3DTwo.Child = viewport3DTwo;
            PageTwo.Controls.Add(elementHost3DTwo);

            //Создание третей вкладки
            ElementHost elementHost3DThree = new ElementHost();
            elementHost3DThree.Dock = DockStyle.Fill;
            arrayCoord = creater.CreatedArrayMeans(3);
            UserViewport3D viewport3DThree = new UserViewport3D(arrayCoord, rows, columns);
            elementHost3DThree.Child = viewport3DThree;
            PageThree.Controls.Add(elementHost3DThree);
        }
    }
}
