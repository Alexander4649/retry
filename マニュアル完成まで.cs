using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace やり直し
{
    public partial class Form1 : Form
    {
        Point Cpos; //座標格納　Point構造体
        Boolean shotFlg; //true:発射準備中
        Boolean hitFlg; //true:当たった
        long score; //スコア

        public Form1()
        {
            InitializeComponent();
        }

        //プレイヤー上移動
        private void movePlayer()
        {
            if (hitFlg)
            {
                return; //プレイヤーの動きを止める
            }

            Player.Top -= 12; //上に移動させるためのY座標使う
            if (Player.Top < (0 - Player.Height))
            {
                score += 10; //画面上部まできたらスコア加算
                scoreLabel.Text = score.ToString();
                Sukima.Width -= 5; //すき間を少し狭くする
                initPlayer(); //プレイヤーの初期化
            }

            if ((Player.Top < Sukima.Bottom) && (Player.Bottom> Sukima.Top))
            {
                if ((Player.Left < Sukima.Left) || (Player.Right > Sukima.Right))
                {
                    hitFlg = true;
                    button1.Enabled = true; //開始ボタンを有効に
                    Gameover.Show(); //ゲームオーバーを表示
                }
            }
            if ((Player.Top < Sukima2.Bottom) && (Player.Bottom > Sukima2.Top))
            {
                if ((Player.Left < Sukima2.Left) || (Player.Right > Sukima2.Right))
                {
                    hitFlg = true;
                    button1.Enabled = true; //開始ボタンを有効に
                    Gameover.Show(); //ゲームオーバーを表示
                }
            }
            if ((Player.Top < Wall.Bottom) && (Player.Bottom > Wall.Top))
            {
                if ((Player.Right > Wall.Right) || (Player.Left < Wall.Left))
                {
                    hitFlg = true;
                    button1.Enabled = true; //開始ボタンを有効に
                    Gameover.Show(); //ゲームオーバーを表示
                }
            }
        }

        //すき間の横移動
        private void moveSukima()
        {
            if (hitFlg)
            {
                return;
            }

            Sukima.Left += 4;
            Sukima2.Left += 2;

            if (Sukima.Left > Width)
            {
                Sukima.Left = -Sukima.Width;
            }
            if (Sukima2.Left > Width)
            {
                Sukima2.Left = -Sukima2.Width;
            }

            /*
            Wall.Left += 1;
            if (Wall.Left > Width)
            {
                Wall.Left = -Wall.Width;
            }

            if (Cpos.X < 0)
            {
                Cpos.X = 5;
            }
            if (Cpos.X > ClientSize.Width - Wall.Width)
            {
                Cpos.X = ClientSize.Width - Wall.Width;
            }*/
        }

        //壁の左右移動
        /*private void moveWall()
        {
            if (hitFlg)
            {
                return;
            }
            Wall.Left += 1;
            if (Wall.Left > Width)
            {
                Wall.Left = -Wall.Width;
            }
        }*/

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cpos = PointToClient(Cursor.Position); //カーソルの画面座標取得
            moveSukima(); //すき間の横移動
            //moveWall(); //壁の横移動

            if (shotFlg)
            {
                movePlayer(); //プレイヤー上移動
                return; //timer1_Tickからぬける ,上移動中に横移動しなくなる
            }
            
            if (Cpos.X < 0)
            {
                Cpos.X = 0;
            }
            if (Cpos.X > ClientSize.Width - Player.Width)
            {
                Cpos.X = ClientSize.Width - Player.Width;
            }
            Player.Left = Cpos.X; //マウスの座標を代入
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initGame();
        }

        //ゲームの初期化
        private void initGame()
        {
            hitFlg = false; //false:当たっていない
            Gameover.Hide(); //ゲームオーバーを隠す
            button1.Enabled = false; //開始ボタンを無効に
            score = 0; //スコアのクリア
            scoreLabel.Text = "0";
            Sukima.Width = 80; //すき間の幅
            initPlayer();
        }

        //プレイヤーの初期化
        private void initPlayer()
        {
            Player.Top = Height - (Player.Height * 2); //Form1の高さ - プレイヤーの高さ2個分
            Player.Left = Cpos.X;
            shotFlg = false; //false:発射していない 画面外へ出たときにinitPlayerが呼ばれ、shotFlgはfalseになる
        }

        private void Player_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initGame();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            shotFlg = true; //true:発射移動中
        }
    }
}
