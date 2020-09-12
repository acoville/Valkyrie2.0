/*==================================================================
 * 
 * Valkyrie 2.0 Game Engine
 * 
 * GamePage ViewModel
 * 
 * functions relating to the evaluation of horizontal motion
 * 
 * ===============================================================*/

using System.ComponentModel;
using System.Linq;
using Valkryie.GL;
using Valkyrie.App.Model;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        //===================================================================

        /*-------------------------------------
         * 
         * Evaluate Vertical Motion
         * 
         * 
         * 
         * -----------------------------------*/

        internal void EvaluateVerticalMotion(Actor actor)
        {
            if(actor.Standing)
            {
                bool jumpCommanded = actor.ControlStatus.Jump;

                if(jumpCommanded)
                {
                    if(actor.AvailableJumps > 0)
                    {
                        actor.Jump();
                    }
                }
            }

            //------ otherwise, actor is not standing so 
            //         must be going up or down 

            else
            {
                EvaluateDown(actor);
            }
        }

        //===================================================================

        /*--------------------------------
         * 
         * What goes up must come down
         * 
         * -----------------------------*/

        public void EvaluateDown(Actor actor)
        {
            actor.Y_Acceleration_Rate -= 1.0f;
            float newYSpeed = actor.Accelerate_Y();

            //--------- we are still on the ascent and need to check for 
            //          collision on objects above

            if(actor.y_speed_ > 0)
            {
                GLPosition topLeftCorner = actor.GLPosition;
                topLeftCorner.Y += actor.Rectangle.PixelHeight;

                var contextQuery = from obstacle in obstacles_
                                   where obstacle.Rectangle.Bottom > actor.Rectangle.Top
                                   orderby obstacle.GLPosition.Vertical_Distance_To(topLeftCorner) ascending
                                   select obstacle;

                Obstacle nearest = contextQuery.First();

                if (actor.Intersects(nearest))
                {
                    float newY = nearest.GLPosition.Y;
                    
                    GLPosition newPosition = new GLPosition(actor.GLPosition.X, newY);
                    actor.MoveTo(newPosition);
                    actor.Stop_Y_Axis_Motion();
                }
            }

            //------------ otherwise, he must be falling and need to check for
            //              collision on objects below
            
            else if(actor.Y_Speed < 0)
            {
                var contextQuery = from obstacle in obstacles_
                                   where obstacle.Rectangle.Top < actor.Rectangle.Bottom
                                   orderby obstacle.GLPosition.Vertical_Distance_To(actor.GLPosition) ascending
                                   select obstacle;

                Obstacle nearest = contextQuery.First();

                if (actor.Intersects(nearest))
                {
                    //-- stop moving 

                    actor.Stop_Y_Axis_Motion();
                    float newY = nearest.GLPosition.Y + nearest.Rectangle.PixelHeight;
                    GLPosition newPosition = new GLPosition(actor.GLPosition.X, newY);
                    actor.MoveTo(newPosition);

                    //-- reset appropriate statuses

                    actor.Standing = true;
                    actor.current_jumps_ = 0;
                }
            }
        }
    }

}
