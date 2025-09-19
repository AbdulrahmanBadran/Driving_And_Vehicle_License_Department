using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class AddUpdateScreen : Form
    {

         //enum eMode{read=1};

        public AddUpdateScreen(int ID,int mode=-1)
        {
            
            InitializeComponent();

           userCard1.ID = ID;

            if(mode==1)
            { 

            
                return;
            }
        }

        public AddUpdateScreen(string nationalNo, int mode = -1)
        {

            InitializeComponent();

            userCard1.NationalNo = nationalNo;

            if (mode == 1)
            {


                return;
            }
        }

        public AddUpdateScreen()
        {

            InitializeComponent();

            
        }

        private void AddUpdateScreen_Load(object sender, EventArgs e)
        {

        }

        private void userCard1_Load(object sender, EventArgs e)
        {

        }

        private void userCard1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
