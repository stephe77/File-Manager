﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Permissions;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace wfaFileManager
{
    public partial class Form1 : Form
    {
        private string filePath = "D:";
        private bool isFile = false;
        private string currentlySelectedItemName = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filePathTextBox.Text = filePath;
            loadFiledAndDirectories();

            //if (fileTypeLabel.Text == ".pdf")
            //{
            //    //string ste = $@"{filePathTextBox.Text}\{fileNameLabel.Text}";
                
            //    //pictureBox1.Image = Image.FromFile($@"{filePathTextBox.Text}\{fileNameLabel.Text}");
            //    this.pdfViewer1.LoadFromFile($@"{filePathTextBox.Text}\{fileNameLabel.Text}");
            //}
        }

        public void loadFiledAndDirectories()
        {
            DirectoryInfo fileList;
            string tempFilePath = "";
            FileAttributes fileAttr;
            try
            {

                if (isFile)
                {
                    tempFilePath = filePath + "/" + currentlySelectedItemName;
                    FileInfo fileDetails = new FileInfo(tempFilePath);
                    fileNameLabel.Text = fileDetails.Name;
                    fileTypeLabel.Text = fileDetails.Extension;
                    fileAttr = File.GetAttributes(tempFilePath);
                    //Process.Start(tempFilePath);

                    //if (fileDetails.Extension != ".jpg" || fileDetails.Extension != ".png" || fileDetails.Extension != ".pdf")
                    //{
                    //    Process.Start(tempFilePath);
                    //}
                    if (fileDetails.Extension != ".jpg" && fileDetails.Extension != ".pdf")
                    {
                        Process.Start(tempFilePath);
                    }
                    if (fileDetails.Extension == ".pdf")
                    {
                        pdfViewer1.LoadFromFile($@"{filePathTextBox.Text}\{fileNameLabel.Text}");
                    }

                }
                else
                {
                    fileAttr = File.GetAttributes(filePath);
                    
                }

                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    fileList = new DirectoryInfo(filePath);
                    FileInfo[] files = fileList.GetFiles();
                    DirectoryInfo[] dirs = fileList.GetDirectories();
                    string fileExtension = "";
                    listView1.Items.Clear();

                    if (fileExtension == ".jpg")
                    {
                        Image.FromFile($@"{filePathTextBox.Text}\{fileNameLabel.Text}");
                    }

                    for (int i = 0; i < files.Length; i++)
                    {
                        fileExtension = files[i].Extension.ToUpper();
                        switch(fileExtension)
                        {
                            case ".ZIP":
                            case ".RAR":
                                listView1.Items.Add(files[i].Name, 8);
                                break;

                            case ".PNG":
                            case ".JPG":

                                //// Создание нового объекта ImageList для хранения картинок
                                //ImageList imageList = new ImageList();
                                //imageList.ImageSize = new Size(48, 48); // Установка размера картинок

                                //// Добавление картинки из jpg-файла в ImageList
                                //Image image = Image.FromFile($@"{filePathTextBox.Text}{fileNameLabel.Text}");
                                //imageList.Images.Add(image);

                                //listView1.LargeImageList = imageList;
                                //listView1.SmallImageList = imageList;

                                //string ste = $@"{filePathTextBox.Text}\{fileNameLabel.Text})";
                                //if (fileTypeLabel.Text == ".jpg" || fileTypeLabel.Text == ".jpeg")
                                //{
                                //    Image.FromFile($@"{filePathTextBox.Text}\{fileNameLabel.Text}");
                                //}
                                //Image.FromFile($@"{filePathTextBox.Text}\{fileNameLabel.Text}");
                                //listView1.Items.Add(files[i].Name, ste);

                                listView1.Items.Add(files[i].Name, 6);
                                break;

                            case ".PDF":
                                listView1.Items.Add(files[i].Name, 5);
                                break;

                            case ".MP4":
                                listView1.Items.Add(files[i].Name, 4);
                                break;

                            case ".MP3":
                                listView1.Items.Add(files[i].Name, 3);
                                break;

                            case ".EXE":
                                listView1.Items.Add(files[i].Name, 1);
                                break;

                            case ".DOC":
                                listView1.Items.Add(files[i].Name, 0);
                                break;

                            default:
                                listView1.Items.Add(files[i].Name, 7);
                                break;

                        }
                        //listView1.Items.Add(files[i].Name, 7);
                    }

                    for (int i = 0; i < dirs.Length; i++)
                    {
                        listView1.Items.Add(dirs[i].Name, 2);
                    }
                }
                else
                {
                    fileNameLabel.Text = this.currentlySelectedItemName;
                }
            }
            catch (Exception e)
            {

            }
        }

        public void loadButtonAction()
        {
            
            removeBackSlash();
            filePath = filePathTextBox.Text;
            loadFiledAndDirectories();
            isFile = false;

            
            
                //imageLocation = filePathTextBox.Text;

                //pictureBox1.ImageLocation = imageLocation;
            
            //pictureBox1.ImageLocation = filePathTextBox.Text;
            //imageLocation = filePathTextBox.Text;

            //pictureBox1.ImageLocation = imageLocation;


        }

        public void removeBackSlash()
        {
            string path = filePathTextBox.Text;
            if (path.LastIndexOf("/") == path.Length - 1)
            {
                filePathTextBox.Text = path.Substring(0, path.Length - 1);
            }
        }

        public void goBack()
        {
            try
            {
                removeBackSlash();
                string path = filePathTextBox.Text;
                path = path.Substring(0, path.LastIndexOf("/"));
                this.isFile = false;
                filePathTextBox.Text = path;
                removeBackSlash();
            }
            catch (Exception e)
            {

            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            loadButtonAction();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentlySelectedItemName = e.Item.Text;

            FileAttributes fileAttr = File.GetAttributes(filePath + "/" + currentlySelectedItemName);
            if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                isFile = false;
                filePathTextBox.Text = filePath + "/" + currentlySelectedItemName;
            }
            else
            {
                isFile = true;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            loadButtonAction();
            if (fileTypeLabel.Text == ".jpg" || fileTypeLabel.Text == ".jpeg")
            {
                pictureBox1.Image = Image.FromFile($@"{filePathTextBox.Text}\{fileNameLabel.Text}");
            }


            //pictureBox1.Image = Image.FromFile(filePath);

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            goBack();
            loadButtonAction();
        }
    }
}
