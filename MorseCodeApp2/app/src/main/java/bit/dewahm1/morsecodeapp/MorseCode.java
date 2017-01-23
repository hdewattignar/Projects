package bit.dewahm1.morsecodeapp;

import android.content.Intent;
import android.content.res.Resources;
import android.graphics.Color;
import android.os.SystemClock;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.RelativeLayout;
import android.widget.TextView;

public class MorseCode extends AppCompatActivity {

    int unit = 500;
    String convertedString = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_morse_code);

        //get string to convert
        String stringToConvert = getIntent().getExtras().getString("input");
        convertedString = convertText(stringToConvert);

        //set the text box text
        TextView showRequestedText = (TextView) findViewById(R.id.textView_raw);
        showRequestedText.setText(stringToConvert);

        TextView showConvertedText = (TextView) findViewById(R.id.textView_converted);
        showRequestedText.setText(convertedString);

        //setup buttons
        Button backButton = (Button)findViewById(R.id.button_Back);
        BackButtonHandler goBack = new BackButtonHandler();
        backButton.setOnClickListener(goBack);

        Button goButton = (Button)findViewById(R.id.button_go);
        RunHandler run = new RunHandler();
        goButton.setOnClickListener(run);



        //Run(convertedString);
    }

    public class RunHandler implements View.OnClickListener
    {

        @Override
        public void onClick(View v) {
            Run(convertedString);
        }
    }

    public void Run(String string)
    {
        RelativeLayout background = (RelativeLayout)findViewById(R.id.Layout);
        for (int i = 0; i < string.length();i++)
        {
            if(string.charAt(i) == '0')
            {
                background.setBackgroundColor(Color.BLACK);
            }
            else
            {
                background.setBackgroundColor(Color.WHITE);
            }
            SystemClock.sleep(unit);
        }
    }

    public String convertText(String textToConvert)
    {
        String returnString = "";

        for(int i = 0; i < textToConvert.length(); i++)
        {
            String current = "";
            switch (textToConvert.charAt(i))
            {
                //letters
                case 'a':
                    current = "10111";
                    break;
                case 'b':
                    current = "111010101";
                    break;
                case 'c':
                    current = "11101011101";
                    break;
                case 'd':
                    current = "1110101";
                    break;
                case 'e':
                    current = "1";
                    break;
                case 'f':
                    current = "101011101";
                    break;
                case 'g':
                    current = "111011101";
                    break;
                case 'h':
                    current = "1010101";
                    break;
                case 'i':
                    current = "101";
                    break;
                case 'j':
                    current = "1011101110111";
                    break;
                case 'k':
                    current = "111010111";
                    break;
                case 'l':
                    current = "101110101";
                    break;
                case 'm':
                    current = "1110111";
                    break;
                case 'n':
                    current = "11101";
                    break;
                case 'o':
                    current = "11101110111";
                    break;
                case 'p':
                    current = "10111011101";
                    break;
                case 'q':
                    current = "1110111010111";
                    break;
                case 'r':
                    current = "1011101";
                    break;
                case 's':
                    current = "10101";
                    break;
                case 't':
                    current = "111";
                    break;
                case 'u':
                    current = "1010111";
                    break;
                case 'v':
                    current = "101010111";
                    break;
                case 'w':
                    current = "101110111";
                    break;
                case 'x':
                    current = "11101010111";
                    break;
                case 'y':
                    current = "1110101110111";
                    break;
                case 'z':
                    current = "11101110101";
                    break;
                //numbers
                case '0':
                    current = "1110111011101110111";
                    break;
                case '1':
                    current = "10111011101110111";
                    break;
                case '2':
                    current = "101011101110111";
                    break;
                case '3':
                    current = "1010101110111";
                    break;
                case '4':
                    current = "10101010111";
                    break;
                case '5':
                    current = "101010101";
                    break;
                case '6':
                    current = "11101010101";
                    break;
                case '7':
                    current = "1110111010101";
                    break;
                case '8':
                    current = "111011101110101";
                    break;
                case '9':
                    current = "11101110111011101";
                    break;
                //space
                case ' ':
                    current = "0000000";
                    break;
            }


            returnString = returnString + current + "000";
        }

        return returnString;
    }


    public class BackButtonHandler implements View.OnClickListener
    {

        @Override
        public void onClick(View v) {
            Intent goBack = new Intent(MorseCode.this, MainActivity.class);
            startActivity(goBack);
        }
    }
}
