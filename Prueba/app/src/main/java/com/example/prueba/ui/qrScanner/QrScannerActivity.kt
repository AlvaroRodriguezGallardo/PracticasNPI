package com.example.prueba.ui.qrScanner

import android.app.Activity
import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.prueba.databinding.ActivityQrScannerBinding
import com.google.zxing.integration.android.IntentIntegrator


class QrScannerActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        //setContentView(R.layout.activity_food)
        val binding: ActivityQrScannerBinding = ActivityQrScannerBinding.inflate(layoutInflater)

        setContentView(binding.root)

        binding.btnScan.setOnClickListener{
            val scanner = IntentIntegrator(this)

            scanner.initiateScan()
        }
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        if(resultCode == Activity.RESULT_OK){
            val result = IntentIntegrator.parseActivityResult(requestCode, resultCode, data)
            if(result != null){
                if(result.contents == null){
                    Toast.makeText(this, "Cancelled", Toast.LENGTH_LONG).show()
                } else{
                    Toast.makeText(this, "Scanned: " + result.contents, Toast.LENGTH_LONG).show()
                }
            }
        } else {
            super.onActivityResult(requestCode, resultCode, data)
        }
    }
}