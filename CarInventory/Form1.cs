using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CarInventory
{
    public partial class Form1 : Form
    {
        List<Car> carsList = new List<Car>();

        public Form1()
        {
            InitializeComponent();
            loadDB();
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveDB();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string year, make, colour, mileage;
            int id = Convert.ToInt16(textBox1.Text);
            year = yearInput.Text;
            make = makeInput.Text;
            colour = colourInput.Text;
            mileage = mileageInput.Text;

            Car car = new Car(id, year, make, colour, mileage);

            carsList.Add(car);

            outputLabel.Text = "";

            //for (int i = 0; i < carsList.Count; i++)
            //{
            //    outputLabel.Text += carsList[i].year + " " + carsList[i].make + " " + carsList[i].colour + " " + carsList[i].mileage + "\n";
            //}

            foreach(Car c in carsList)
            {
                outputLabel.Text += c.year + " "
                    + c.make + " "
                    + c.colour + " "
                    + c.mileage + "\n";
            }
        }

        public void loadDB()
        {
            int newID;
            string newYear, newMake, newColour, newMileage;

            XmlReader reader = XmlReader.Create("Resources/level2.xml");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                   newID = Convert.ToInt16(reader.ReadString());

                   reader.ReadToNextSibling("year");
                    newYear = reader.ReadString();

                    reader.ReadToNextSibling("make");
                    newMake = reader.ReadString();

                    reader.ReadToNextSibling("mileage");
                    newColour = reader.ReadString();

                    reader.ReadToNextSibling("colour");
                    newMileage = reader.ReadString();

                    Car newCar = new Car(newID, newYear, newMake, newColour, newMileage);
                    carsList.Add(newCar);
                }
            }

            reader.Close();
        }
        public void saveDB()
        {
            XmlWriter writer = XmlWriter.Create("Resources/level2.xml", null);

            writer.WriteStartElement("cars");

            foreach (Car c in carsList)
            {
                writer.WriteStartElement("car");

                writer.WriteElementString("id", Convert.ToString(c.id));
                writer.WriteElementString("year", c.year);
                writer.WriteElementString("make", c.make);
                writer.WriteElementString("mileage", c.mileage);
                writer.WriteElementString("colour", c.colour);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int searchID = Convert.ToInt16(textBox1.Text);

            int index = carsList.FindIndex(car => car.id == searchID);

            if (index >= 0)
            {
                carsList.RemoveAt(index);
                outputLabel.Text = "Employee " + searchID + " removed";
            }
            else
            {
                outputLabel.Text = "Employee ID not found";
            }
        }

        
    }
}
