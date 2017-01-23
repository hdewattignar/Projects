package bit.dewahm1.morsecodeapp;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RelativeLayout;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        //set up text box
        TextView textToConvert = (TextView) findViewById(R.id.editText);


        //setup button
        Button goButton = (Button) findViewById(R.id.button_convert);
        SetTextToConvertHandler converText = new SetTextToConvertHandler();
        goButton.setOnClickListener(converText);


    }

    public class SetTextToConvertHandler implements View.OnClickListener{

        @Override
        public void onClick(View v) {

            //get text from text box
            EditText textInput = (EditText)findViewById(R.id.editText);
            String inputString = textInput.getText().toString();

            //check string is valid

            //got to the next page passing the text
            Intent goToMorseCode = new Intent(MainActivity.this, MorseCode.class);
            goToMorseCode.putExtra("input", inputString);
            startActivity(goToMorseCode);

        }
    }
}
