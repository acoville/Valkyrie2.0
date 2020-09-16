/*==================================================================
 * 
 * Valkyrie 2.0 Game Engine
 * 
 * GamePage ViewModel
 * 
 * functions relating to the evaluation of horizontal motion
 * 
 * ===============================================================*/

using System;
using System.ComponentModel;
using System.Linq;
using System.Security;
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

            else // actor is not standing
            {
                if(actor.Y_Speed > 0)
                {
                    EvaluateUp(actor);
                }
                else
                {
                    EvaluateDown(actor);
                }
            }
        }

        //=========================================================================

        /*--------------------------------
         * 
         * if Y_Speed > 0 then the actor 
         * is still moving up 
         * 
         * -----------------------------*/

        public void EvaluateUp(Actor actor)
        {
            actor.Y_Acceleration_Rate -= 1.0f;

            var contextQuery = from obstacle in obstacles_
                               where obstacle.Is_Above(actor)
                               orderby actor.Vertical_Distance_Above(obstacle) ascending
                               select obstacle;

            if(contextQuery.Any())
            {
                Obstacle nearest = contextQuery.First();

                if (actor.Intersects(nearest))
                {
                    float newY = nearest.GLPosition.Y;

                    GLPosition newPosition = new GLPosition(actor.GLPosition.X, newY);
                    actor.MoveTo(newPosition);
                    actor.Stop_Y_Axis_Motion();
                }
            }
        }

        //=========================================================================

        /*--------------------------------
         * 
         * if Y_Speed is negative then 
         * the actor must be falling
         * 
         * -----------------------------*/

        public void EvaluateDown(Actor actor)
        {
            var contextQuery = from obstacle in obstacles_
                                where obstacle.Is_Below(actor)
                                orderby actor.Vertical_Distance_Below(obstacle) ascending
                                select obstacle;

            if(contextQuery.Any())
            {
                Obstacle nearest = contextQuery.First();

                //---- condition #1: we are already intersecting

                if (actor.Intersects(nearest))
                {
                    //-- stop moving 

                    float newY = nearest.GLPosition.Y + nearest.Rectangle.PixelHeight;
                    GLPosition newPosition = new GLPosition(actor.GLPosition.X, newY);
                    actor.MoveTo(newPosition);
                    actor.Land();
                }

                //---- condition #2: we will be intersecting in the next frame

                /*
                else
                {
                    var vertical_clearance = actor.Vertical_Distance_Below(nearest);
                    var newYSpeed = actor.NextDeltaY();

                    var margin = vertical_clearance - newYSpeed;

                    if(margin <= 32)
                    {
                        //-- stop moving 

                        float newY = nearest.GLPosition.Y + nearest.Rectangle.PixelHeight;
                        GLPosition newPosition = new GLPosition(actor.GLPosition.X, newY);
                        actor.MoveTo(newPosition);
                        actor.Land();
                    }
                }
                 */
            }
        }
    }
}
