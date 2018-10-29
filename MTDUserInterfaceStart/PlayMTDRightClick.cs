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
        int playerLoadCount = 0;
        int compLoadCount = 0;
        
        



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
            playersHand.Play(d, train);
            //playersTrainPBS.RemoveAt(index);
            
            RemovePBFromForm(userHandPBs[index]);
            userHandPBs.RemoveAt(index);

            //userHandPBs.RemoveAt(index);
            ////playersTrainPBS.RemoveAt(index);

            //RemovePBFromForm(playersTrainPBS[index]);
            //playersHand.Play(d, train);
            //LoadDomino(trainPBs[0], d);
            AddToPB(train,trainPBs,d);

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
            bool played = false;
            bool mustFlip = false;
            try
            {


                foreach (Domino d in computersHand)
                {
                    if(computersTrain.IsPlayable(computersHand,d,out mustFlip)==true)
                    {
                        if (mustFlip == true)
                            d.Flip();
                        computersHand.Play(computersTrain);
                        computersTrain.Add(d);

                        AddToPB(computersTrain, computerTrainPBS, d);
                        //LoadDomino(computerTrainPBS[0], d);
                        played = true;
                        break;
                    }
                    else if (mexicanTrain.IsPlayable(computersHand, d, out mustFlip) == true)
                    {
                        if (mustFlip == true)
                            d.Flip();
                        computersHand.Play(mexicanTrain);
                        mexicanTrain.Add(d);

                        AddToPB(mexicanTrain, mexicanTrainPBS, d);
                        //LoadDomino(computerTrainPBS[0], d);
                        played = true;
                        break;
                    }
                    else if (playersTrain.IsOpen == true)
                    {
                        if (playersTrain.IsPlayable(computersHand, d, out mustFlip) == true)
                        {
                            if (mustFlip == true)
                                d.Flip();
                            computersHand.Play(playersTrain);
                            AddToPB(playersTrain, playersTrainPBS, d);
                            //LoadDomino(playersTrainPBS[0], d);played = true;
                            break;
                        }
                    }

                }
            }
            catch
            {


                if (played == false)//makes draw and then play
                {


                    computersHand.Draw(boneyard);
                    Domino drawedD = computersHand[computersHand.Count - 1];
                    if (playersTrain.IsOpen == true && playersTrain.IsPlayable(computersHand, drawedD, out mustFlip) == true)
                    {
                        if (mustFlip == true)
                            drawedD.Flip();
                        computersHand.Play(playersTrain);
                        AddToPB(computersTrain, computerTrainPBS, drawedD);
                        //LoadDomino(playersTrainPBS[0], drawedD);

                    }
                    else if (computersTrain.IsPlayable(computersHand, drawedD, out mustFlip) == false)
                    {

                        if (mustFlip == true)
                            drawedD.Flip();
                        computersHand.Play(playersTrain);
                        AddToPB(computersTrain, computerTrainPBS, drawedD);
                        //LoadDomino(playersTrainPBS[0], drawedD);

                    }
                    else
                        MessageBox.Show("The Computer Passes.");


                }
                
                    
            }


            EnableUserMove();
            return true;
        }
        /// <summary>
        /// needed some sort of method to add and update the pictureboxes for when they ahve more than 5
        /// seems more sense to make a method for it than having to constatly repeat code
        /// many thanks to kaiser for helping me with this code.
        /// </summary>
        /// <param name="train"></param>
        /// <param name="trainPB"></param>
        public void AddToPB(Train train, List<PictureBox> trainPB, Domino d)
        {
            if (train.Count >= 5)
            {
                for (int i = 0; i < trainPB.Count; i++)
                {
                    LoadDomino(trainPB[i], train[train.Count - (5 - i)]);
                }
            }
            else if (train.Count.Equals(0)) 
                LoadDomino(trainPB[0], d);
            else
                LoadDomino(trainPB[train.Count - 1],train[train.Count-1]);
            //loadCount++;

        }
        // update labels on the UI and disable the users hand
        // call MakeComputerMove (maybe twice)
        // update the labels on the UI
        // determine if the computer won or if it's now the user's turn
        public void CompleteComputerMove()
        {
            bool canFlip = false;
            computerHandLabel.Text = computersHand.Count.ToString();
            userLabel.ForeColor = Color.Red;
            computerLabel.ForeColor = Color.Green;
            
            //System.Threading.Thread.Sleep(10000);//makes AI appear to be thinking?
            MakeComputerMove(false);
            if (computersHand.Count == 0||playersHand.Count==0||boneyard.IsEmpty()==true)
                Win();
            else
                    EnableUserMove();
            computerHandLabel.Text = computersHand.Count.ToString();
            boneyardCountLabel.Text = boneyard.Count<Domino>().ToString();
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
            bool canDraw = false;
            boneyard = new BoneYard(9);
            boneyard.Shuffle();
            boneyard.AlmostEmptyBY += new BoneYard.ChangeHandlerBY(RespondToEmpty);
            playersHand = new Hand(boneyard, 2);
            computersHand = new Hand(boneyard, 2);
            Domino highestDouble;
            userHandPBs = new List<PictureBox>(10);
            mexicanTrainPBS = new List<PictureBox> { mexTrainPB1,mexTrainPB2,mexTrainPB3,mexTrainPB4,mexTrainPB5 };
            playersTrainPBS = new List<PictureBox> { userTrainPB1, userTrainPB2, userTrainPB3, userTrainPB4, userTrainPB5 };
            //playersTrainPBS = new List<PictureBox> { userTrainPB5, userTrainPB4, userTrainPB3, userTrainPB2, userTrainPB1 };
            computerTrainPBS = new List<PictureBox> { compTrainPB1, compTrainPB2, compTrainPB3, compTrainPB4, compTrainPB5 };

            for (int i = 0; i <playersHand.Count; i++, nextDrawIndex++)
            {

                userHandPBs.Add(CreateUserHandPB(nextDrawIndex));
                
            }
            LoadHand(userHandPBs, playersHand);
            int pDIndex = playersHand.IndexOfHighDouble();
            int cDIndex = computersHand.IndexOfHighDouble();

            bool playerFirst;
            if(pDIndex==-1)
            {
                //comp goes first
                playerFirst = false;
                highestDouble = computersHand[cDIndex];
            }
            else if(cDIndex==-1)
            {
                //player goes first
                playerFirst = true;
                highestDouble = playersHand[pDIndex];
            }
            else if (playersHand[pDIndex].Side1 > computersHand[cDIndex].Side1)
            {
                //player goes first
                playerFirst = true;
                highestDouble = playersHand[pDIndex];
            }
            else
            {
                //comp goes firt.
                playerFirst = false;
                highestDouble = computersHand[cDIndex];
            }
            playersTrain = new PlayerTrain(playersHand, highestDouble.Side1);
            computersTrain = new PlayerTrain(computersHand, highestDouble.Side1);
            mexicanTrain = new MexicanTrain(highestDouble.Side1);
            playersTrain.Close();
            userTrainStatusLabel.Text = CLOSED;
            userTrainStatusLabel.ForeColor = Color.Red;
            computersTrain.Close();
            computerTrainStatusLabel.Text = CLOSED;
            computerTrainStatusLabel.ForeColor = Color.Red;
            computerHandLabel.Text = computersHand.Count.ToString();

            if (playerFirst==true)
            {
                //player goes first              
                playersHand.RemoveAt(pDIndex);
                //userHandPBs.RemoveAt(pDIndex);
                LoadDomino(enginePB, highestDouble);
                //playersTrainPBS.RemoveAt(pDIndex);
                userLabel.ForeColor = Color.Green;
                computerLabel.ForeColor = Color.Red;
                PictureBox pb = userHandPBs[pDIndex];
                RemoveDominoFromPB(pDIndex, userHandPBs);
                userHandPBs.RemoveAt(pDIndex);
                RemovePBFromForm(pb);
                EnableUserMove();
            }
            else
            {
                //Computer goes first
                
                
                computersHand.RemoveAt(cDIndex);
                LoadDomino(enginePB, highestDouble);
                userLabel.ForeColor = Color.Red;
                computerLabel.ForeColor = Color.Green;
                CompleteComputerMove();
            }
            boneyardCountLabel.Text = boneyard.Count<Domino>().ToString();

            //for(int i=0;i<10;i++)
            //{

            //    userHandPBs.Add(CreateUserHandPB(i));
            //    nextDrawIndex++;
            //}
            //LoadHand(userHandPBs, playersHand);
            //playersTrain.Close();
            //userTrainStatusLabel.Text = CLOSED;
            //userTrainStatusLabel.ForeColor = Color.Red;
            //computersTrain.Close();
            //computerTrainStatusLabel.Text = CLOSED;
            //computerTrainStatusLabel.ForeColor = Color.Red;
            //computerHandLabel.Text = computersHand.Count.ToString();

        }

        // remove all of the domino pictures for each train
        // remove all of the domino pictures for the user's hand
        // reset the nextDrawIndex to 15
        public void TearDown()
        {
            nextDrawIndex =15;
            Hand playersHand = new Hand(boneyard,2);
            Hand computersHand = new Hand(boneyard, 2);
            foreach(PictureBox pb in userHandPBs)
            {
                RemovePBFromForm(pb);
            }
            foreach (PictureBox pb in playersTrainPBS)
            {
                RemovePBFromForm(pb);
            }
            foreach (PictureBox pb in mexicanTrainPBS)
            {
                RemovePBFromForm(pb);
            }
            foreach (PictureBox pb in computerTrainPBS)
            {
                RemovePBFromForm(pb);
            }




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
                if (computersTrain.IsPlayable(playersHand, userDominoInPlay, out mustFlip)&&computersTrain.IsOpen==true)
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
            UserPlayOnTrain(playersHand[indexOfDominoInPlay], mexicanTrain, mexicanTrainPBS);
            // RemovePBFromForm(d);
            DisableUserHandPBs();
            drawButton.Enabled = false;
            passButton.Enabled = false;
            CompleteComputerMove();
            
        }

        // play on the computer train, lets the computer take a move and then enables
        // hand pbs so the user can make the next move.
        private void computerTrainItem_Click(object sender, EventArgs e)
        {
            
            if (computersTrain.IsOpen == true)
            {


                UserPlayOnTrain(playersHand[indexOfDominoInPlay], computersTrain, computerTrainPBS);
                DisableUserHandPBs();
                
                drawButton.Enabled = false;
                passButton.Enabled = false;
                CompleteComputerMove();

            }
            else
                MessageBox.Show("The computers train is closed please play on an open train.");
        }

        // play on the user train, lets the computer take a move and then enables
        // hand pbs so the user can make the next move.
        private void myTrainItem_Click(object sender, EventArgs e)
        {
            
            
            
            UserPlayOnTrain(playersHand[indexOfDominoInPlay], playersTrain, playersTrainPBS);
           
            
            //RemovePBFromForm(d);
            DisableUserHandPBs();

            //drawButton.Enabled = false;
            //passButton.Enabled = false;
            CompleteComputerMove();
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
            //playersHand.Draw(boneyard);
            //int index = playersHand.Count-1;

            //PictureBox pB=(CreateUserHandPB(nextDrawIndex));
            //userHandPBs.Add(pB);
            //nextDrawIndex++;
            //LoadDomino(userHandPBs[index], playersHand[index]);

            ////////////////////////////////////////////////////////
            if (computersHand.Count == 0 || playersHand.Count == 0 || boneyard.IsEmpty() == true)
                Win();
            else
            {

                playersHand.Draw(boneyard);
                int index = playersHand.Count - 1;

                PictureBox pb = CreateUserHandPB(nextDrawIndex);
                userHandPBs.Add(pb);
                EnableHandPB(pb);

                //userHandPBs.Add(CreateUserHandPB(nextDrawIndex));
                nextDrawIndex++;
                LoadDomino(pb, playersHand[index]);
                //drawButton.Enabled = false;
                boneyardCountLabel.Text = boneyard.Count<Domino>().ToString();
            }
            drawButton.Enabled = false;
            

        }

        // open the user's train, update the ui and let the computer make a move
        // enable the hand pbs so the user can make a move
        private void passButton_Click(object sender, EventArgs e)
        {

            drawButton.Enabled = false;
            passButton.Enabled = false;
            playersTrain.Open();
            userTrainStatusLabel.Text = OPEN;
            userTrainStatusLabel.ForeColor = Color.Green;
            DisableUserHandPBs();
            CompleteComputerMove();
            drawButton.Enabled = true;
            passButton.Enabled = true;
            

        }
        /// <summary>
        /// counts up scores and what not
        /// </summary>
        public void Win()
        {
            string winner = "";
            int playerScore, compScore;
            playerScore = playersHand.Score;
            compScore = computersHand.Score;
            if (playerScore < compScore)
                winner = "player wins!";
            else if (playerScore > compScore)
                winner = "computer wins!";
            else
                winner = ("It is a tie");
            MessageBox.Show("The Game is over the " + winner + " The player had " + playerScore + " points and the computer had " + compScore + " points.");
            DisableUserHandPBs();
            drawButton.Enabled = false;
            passButton.Enabled = false;

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
