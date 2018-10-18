using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTDClasses;

namespace MTDUserInterface
{
    public partial class PlayMTDRightClick : Form
    {
        #region instance_variables

        #endregion
        BoneYard boneyard;
        Hand computersHand;
        Hand playersHand;
        MexicanTrain mexicanTrain;
        PlayerTrain playersTrain, computersTrain;
        List<PictureBox> userHandPBs;
        List<PictureBox> computerTrainPBS,playersTrainPBS, mexicanTrainPBS;
        int nextDrawIndex = 0;
        Domino userDominoInPlay;
        int indexOfDominoInPlay;
        const string OPEN = "Open";
        const string CLOSED = "Closed";
        



        #region Methods

        // loads the image of a domino into a picture box
        // verify that the path for the domino files is correct
        private void LoadDomino(PictureBox pb, Domino d)
        {
            pb.Image = Image.FromFile(System.Environment.CurrentDirectory
                        + "\\..\\..\\Dominos\\" + d.Filename);
        }

        // loads all of the dominos in a hand into a list of pictureboxes
        private void LoadHand(List<PictureBox> pbs, Hand h)
        {
            for (int i = 0; i < pbs.Count; i++)
            {
                PictureBox pb = pbs[i];
                Domino d = h[i];
                LoadDomino(pb, d);
            }
        }

        // dynamically creates the "next" picture box for the user's hand
        // the instance variable nextDrawIndex should be passed as a parameter
        // if you change the layout of the form, you'll have to change the math here
        private PictureBox CreateUserHandPB(int index)
        {
            PictureBox pb = new PictureBox();
            pb.Visible = true;
            pb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pb.Location = new System.Drawing.Point(24 + (index % 5) * 110, 366 + (index / 5) * 60);
            pb.Size = new System.Drawing.Size(100, 50);
            pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Controls.Add(pb);
            return pb;
        }

        // adds the mouse down event handler to a picture box
        private void EnableHandPB(PictureBox pb)
        {
            pb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.handPB_MouseDown);
        }

        // adds the mouse down event handler to all of the picture boxes in the users hand pb list
        private void EnableUserHandPBs()
        {
            for (int i = 0; i < userHandPBs.Count; i++)
            {
                PictureBox pb = userHandPBs[i];
                EnableHandPB(pb);
            }
        }

        // removes the mouse down event handler from a picture box
        private void DisableHandPB(PictureBox pb)
        {
            pb.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.handPB_MouseDown);
        }

        // removes all of the mouse down event handlers from the picture boxes in the users hand pb list
        private void DisableUserHandPBs()
        {
            for (int i = 0; i < userHandPBs.Count; i++)
            {
                PictureBox pb = userHandPBs[i];
                DisableHandPB(pb);
            }
        }

        // unloads the domino image from a picture box in a train
        public void RemoveDominoFromPB(int index, List<PictureBox> pBs)
        {
            PictureBox pB = pBs[index];
            pB.Image = null;
        }
		
		// removes a picture box from the form dynamically
		public void RemovePBFromForm(PictureBox pb)
		{
            PictureBox pB = pb;
			this.Controls.Remove(pB);
            pb = null;
		}

        // plays a domino on a train.  Loads the appropriate train pb, 
        // removes the domino pb from the hand, updates the train status label ,
        // disables the hand pbs and disables appropriate buttons
        public void UserPlayOnTrain(Domino d, Train train, List<PictureBox> trainPBs)
        {

            int index=playersHand.IndexOfDomino(d.Side1, d.Side2);
            playersTrainPBS.RemoveAt(index);

            RemovePBFromForm(playersTrainPBS[index]);
            playersHand.Play(d, train);
            LoadDomino(trainPBs[0], d);
            

            DisableUserHandPBs();
            passButton.Enabled = false;
            drawButton.Enabled = false;

        }

        // adds a domino picture to a train
        public void ComputerPlayOnTrain(Domino d, Train train, List<PictureBox> trainPBs, int pbIndex)
        {
        }

        // ai for computer move.
        // calls play for on the computer's hand for each train, gets the next pb index and 
        // plays the domino on the train.  
        // BECAUSE play throws an exception, tries to first play on one train and
        // in the catch block tries the next and so on
        // when the computer can not play on any train, the computer draws and returns false.
        // if the method is called with canDraw = false, this last step is omitted and the method
        // returns false
        public bool MakeComputerMove(bool canDraw)
        {
            bool mustFlip = false;
            foreach(Domino d in computersHand)
            {
                if(mexicanTrain.IsPlayable(computersHand, d, out mustFlip)==true)
                {
                    if (mustFlip == true)
                        d.Flip();
                    computersHand.Play(mexicanTrain);
                    break;
                }
                else if(playersTrain.IsOpen==true)
                {
                    if(playersTrain.IsPlayable(computersHand, d, out mustFlip)==true)
                    {
                        if (mustFlip == true)
                            d.Flip();
                        computersHand.Play(playersTrain);
                        break;
                    }
                }
                else
                {
                    computersHand.Draw(boneyard);
                    Domino drawedD = computersHand[computersHand.Count - 1];
                    if (playersTrain.IsPlayable(computersHand, drawedD, out mustFlip) == true)
                    {
                        if (mustFlip == true)
                            d.Flip();
                        computersHand.Play(playersTrain);
                        break;
                    }
                    else
                    {
                        if (computersTrain.IsPlayable(computersHand, drawedD, out mustFlip)==false)
                            break;
                    }
                    

                }

            }
			return true;
        }

        // update labels on the UI and disable the users hand
        // call MakeComputerMove (maybe twice)
        // update the labels on the UI
        // determine if the computer won or if it's now the user's turn
        public void CompleteComputerMove()
        {
            userLabel.ForeColor = Color.Red;
            computerLabel.ForeColor = Color.Green;
            System.Threading.Thread.Sleep(10000);//makes AI appear to be thinking?
            MakeComputerMove(false);
            if (computersHand.Count == 0)
                Win();
            else
                EnableUserMove();



        }

        // enable the hand pbs, buttons and update labels on the UI
        public void EnableUserMove()
        {
            userLabel.ForeColor = Color.Green;
            computerLabel.ForeColor = Color.Red;
            EnableUserHandPBs();
            drawButton.Enabled = true;
            passButton.Enabled = true;
        }

        // instantiate boneyard and hands
        // find the highest double in each hand
        // determine who should go first, remove the highest double from the appropriate hand
        // and display the highest double in the UI
        // instantiate trains now that you know the engine value
        // create all of the picture boxes for the user's hand and load the dominos for the hand
        // Add the picture boxes for each train to the appropriate list of picture boxes
        // update the labels on the UI
        // if it's the computer's turn, let her play
        // if it's the user's turn, enable the pbs so she can play
        public void SetUp()
        {
            boneyard = new BoneYard(9);
            boneyard.Shuffle();
            playersHand = new Hand(boneyard, 2);
            computersHand = new Hand(boneyard, 2);
            Domino highestDouble;
            userHandPBs = new List<PictureBox>(10);
            mexicanTrainPBS = new List<PictureBox> { mexTrainPB1,mexTrainPB2,mexTrainPB3,mexTrainPB4,mexTrainPB5 };
            playersTrainPBS = new List<PictureBox> { userTrainPB1, userTrainPB2, userTrainPB3, userTrainPB4, userTrainPB5 };
            computerTrainPBS = new List<PictureBox> { compTrainPB1, compTrainPB2, compTrainPB3, compTrainPB4, compTrainPB5 };

            for (int i = 0; i < 10; i++)
            {

                userHandPBs.Add(CreateUserHandPB(i));
                nextDrawIndex++;
            }
            LoadHand(userHandPBs, playersHand);
            int pDIndex = playersHand.IndexOfHighDouble();
            int cDIndex = computersHand.IndexOfHighDouble();
            if(cDIndex==-1||playersHand[pDIndex].Side1>computersHand[cDIndex].Side1)
            {
                //player goes first
                highestDouble = playersHand[pDIndex];
                playersHand.RemoveAt(pDIndex);
                LoadDomino(enginePB, highestDouble);
                //playersTrainPBS.RemoveAt(pDIndex);
                userLabel.ForeColor = Color.Green;
                computerLabel.ForeColor = Color.Red;
                EnableUserMove();
            }
            else
            {
                //Computer goes first
                highestDouble = computersHand[cDIndex];
                computersHand.RemoveAt(cDIndex);
                LoadDomino(enginePB, highestDouble);
                userLabel.ForeColor = Color.Red;
                computerLabel.ForeColor = Color.Green;
                //MakeComputerMove();
            }
            playersTrain = new PlayerTrain(playersHand, highestDouble.Side1);
            computersTrain = new PlayerTrain(computersHand, highestDouble.Side1);
            //for(int i=0;i<10;i++)
            //{

            //    userHandPBs.Add(CreateUserHandPB(i));
            //    nextDrawIndex++;
            //}
            //LoadHand(userHandPBs, playersHand);
            playersTrain.Close();
            userTrainStatusLabel.Text = CLOSED;
            userTrainStatusLabel.ForeColor = Color.Red;
            computersTrain.Close();
            computerTrainStatusLabel.Text = CLOSED;
            computerTrainStatusLabel.ForeColor = Color.Red;

        }

        // remove all of the domino pictures for each train
        // remove all of the domino pictures for the user's hand
        // reset the nextDrawIndex to 15
        public void TearDown()
        {
            
        }
        #endregion

        public PlayMTDRightClick()
        {
            InitializeComponent();
            //SetUp();
        }

        // when the user right clicks on a domino, a context sensitive menu appears that
        // let's the user know which train is playable.  Green means playable.  Red means not playable.
        // the event handler for the menu item is enabled and disabled appropriately.
        private void whichTrainMenu_Opening(object sender, CancelEventArgs e)
        {
            userDominoInPlay = (Domino)sender;
            bool mustFlip = false;
            if (userDominoInPlay != null)
            {
                mexicanTrainItem.Click -= new System.EventHandler(this.mexicanTrainItem_Click);
                computerTrainItem.Click -= new System.EventHandler(this.computerTrainItem_Click);
                myTrainItem.Click -= new System.EventHandler(this.myTrainItem_Click);

                if (mexicanTrain.IsPlayable(playersHand, userDominoInPlay, out mustFlip))
                {
                    mexicanTrainItem.ForeColor = Color.Green;
                    mexicanTrainItem.Click += new System.EventHandler(this.mexicanTrainItem_Click);
                }
                else
                {
                    mexicanTrainItem.ForeColor = Color.Red;
                } 
                if (computersTrain.IsPlayable(playersHand, userDominoInPlay, out mustFlip))
                {
                    computerTrainItem.ForeColor = Color.Green;
                    computerTrainItem.Click += new System.EventHandler(this.computerTrainItem_Click);
                }
                else
                {
                    computerTrainItem.ForeColor = Color.Red;
                }
                if (playersTrain.IsPlayable(playersHand, userDominoInPlay, out mustFlip))
                {
                    myTrainItem.ForeColor = Color.Green;
                    myTrainItem.Click += new System.EventHandler(this.myTrainItem_Click);
                }
                else
                {
                    myTrainItem.ForeColor = Color.Red;
                }
            }
			
        }

        // displays the context sensitve menu with the list of trains
        // sets the instance variables indexOfDominoInPlay and userDominoInPlay
        private void handPB_MouseDown(object sender, MouseEventArgs e)
        {
			
            PictureBox handPB = (PictureBox)sender;
            indexOfDominoInPlay = userHandPBs.IndexOf(handPB);
            if (indexOfDominoInPlay != -1)
            {
                userDominoInPlay = playersHand[indexOfDominoInPlay];
                if (e.Button == MouseButtons.Right)
                {
                    whichTrainMenu.Show(handPB, 
                        handPB.Size.Width - 20, handPB.Size.Height - 20);
                }
            }
			
        }

        // play on the mexican train, lets the computer take a move and then enables
        // hand pbs so the user can make the next move.
        private void mexicanTrainItem_Click(object sender, EventArgs e)
        {
        }

        // play on the computer train, lets the computer take a move and then enables
        // hand pbs so the user can make the next move.
        private void computerTrainItem_Click(object sender, EventArgs e)
        {
        }

        // play on the user train, lets the computer take a move and then enables
        // hand pbs so the user can make the next move.
        private void myTrainItem_Click(object sender, EventArgs e)
        {
        }

        // tear down and then set up
        private void newHandButton_Click(object sender, EventArgs e)
        {
            TearDown();
            SetUp();
        }

        // draw a domino, add it to the hand, create a new pb and enable the new pb
        private void drawButton_Click(object sender, EventArgs e)
        {
            playersHand.Draw(boneyard);
            int index = playersHand.Count-1;
           
            PictureBox pB=(CreateUserHandPB(nextDrawIndex));
            userHandPBs.Add(pB);
            nextDrawIndex++;
            LoadDomino(userHandPBs[index], playersHand[index]);
            
        }

        // open the user's train, update the ui and let the computer make a move
        // enable the hand pbs so the user can make a move
        private void passButton_Click(object sender, EventArgs e)
        {
            playersTrain.Open();
            userTrainStatusLabel.Text = OPEN;
            userTrainStatusLabel.ForeColor = Color.Green;
            DisableUserHandPBs();
            CompleteComputerMove();
        }
        /// <summary>
        /// counts up scores and what not
        /// </summary>
        public void Win()
        {

        }

        private void PlayMTDRightClick_Load(object sender, EventArgs e)
        {
            // register the boneyard almost empty event and it's delegate here
            SetUp();
        }

		// event handler for handling the boneyard almost empty event
        private void RespondToEmpty(BoneYard by)
        {
            MessageBox.Show("The Boneyard must be empty");
        }

    }
}
