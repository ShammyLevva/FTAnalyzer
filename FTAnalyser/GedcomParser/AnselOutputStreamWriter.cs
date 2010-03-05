/*
* AnselOutputStreamWriter<BR>
* This class produces an output stream of bytes representing
* ANSEL-encoded characters, from UNICODE characters supplied as
* input.
* @Author: mhkay@iclway.co.uk
* @Version: 20 May 1998
*/

// 20 May 1998: conversion tables updated with input from John Cowan (cowan@locke.ccil.org)

using System.IO;
namespace FTAnalyzer
{

    public class AnselOutputStreamWriter : StreamWriter
    {

        public AnselOutputStreamWriter(Stream output) : base(output, new AnselEncoding())
        {
        }

    }
}