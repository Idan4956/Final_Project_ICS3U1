using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

public class ClassSelectScreen ()
{
    MouseState mouse;
    MouseState prevMouse;

    int classSelectNum = 1;

    string classCycleNext = ">";
    string classCyclePrev = "<";
    string classSelect = "Fighter";
    string classSelectConfirm = "Confirm";
    string characterName = "";

    char characterChar;

    //can create multiple properties of same class 
    KeyboardState keyboard, oldKeyboard;
    

    Vector2 classCycleNextLoc =  new Vector2(600, 500);
    Vector2 classCyclePrevLoc =  new Vector2(450, 500);
    Vector2 classSelectLoc =  new Vector2(500, 500);
    Vector2 classSelectConfirmLoc =  new Vector2(500, 600);
    Vector2 characterNameLoc = new Vector2(500, 600);

    Rectangle classCycleNextRec = new Rectangle (600, 500, 50, 50);
    Rectangle classCyclePrevRec = new Rectangle (450, 500, 50, 50);
    Rectangle classSelectConfirmRec = new Rectangle (500, 700, 100, 50);
    Rectangle characterNameRec = new Rectangle (500, 600, 100, 50);

    public void update (GameTime gameTime)
    {
        oldKeyboard = keyboard;
        keyboard = Keyboard.GetState();

        prevMouse = mouse;
        mouse = Mouse.GetState();

        if (classSelectNum == 1)
        {
            classSelect = "Fighter";        
        }
        else if (classSelectNum == 2)
        {
            classSelect = "Ranger";    
        }
        else if (classSelectNum == 3)
        {
            classSelect = "Rogue";
        }
        else if (classSelectNum == 4)
        {
            classSelect = "Wizard";
        }
        else if (classSelectNum == 5)
        {
            classSelect = "Cleric";
        }

        if (mouse.LeftButton == ButtonState.Pressed && !(prevMouse.LeftButton == ButtonState.Pressed))
        {
            if (classCycleNextRec.Contains(mouse.Position) && classSelectNum < 5)
            {
                classSelectNum++;
            }
            else if (classCycleNextRec.Contains(mouse.Position) && classSelectNum >= 5)
            {
                classSelectNum = 1;
            }
            if (classCyclePrevRec.Contains(mouse.Position) && classSelectNum > 1)
            {
                classSelectNum--;
            }
            else if (classCyclePrevRec.Contains(mouse.Position) && classSelectNum <= 1)
            {
                classSelectNum = 5;
            }
        }   
        //if (characterNameRec.Contains(mouse.Position))
            {
                if (KeyBoardInput.TryConvertKeyboardInput(keyboard, oldKeyboard, out characterChar))
                {
                    characterName += characterChar;
                }
            }      
    }

    public void draw(SpriteBatch _spriteBatch, SpriteFont font)
    {
        _spriteBatch.DrawString(font, classCycleNext, classCycleNextLoc, Color.White);
        _spriteBatch.DrawString(font, classCyclePrev, classCyclePrevLoc, Color.White);
        _spriteBatch.DrawString(font, classSelect, classSelectLoc, Color.White);
        _spriteBatch.DrawString(font, classSelectConfirm, classSelectConfirmLoc, Color.White);
        _spriteBatch.DrawString(font, characterName, characterNameLoc, Color.White);
    }
}