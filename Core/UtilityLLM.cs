using Markdig;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// markdig license
// https://github.com/xoofx/markdig
// BSD-2-Clause license
/*

Copyright (c) 2018-2019, Alexandre Mutel
All rights reserved.

Redistribution and use in source and binary forms, with or without modification
, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this 
   list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, 
   this list of conditions and the following disclaimer in the documentation 
   and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL 
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER 
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

namespace Core
{
    public static class UtilityLLM
    {
        /// <summary>
        /// Extracts bullet list items out of markdown, ignores any other text
        /// </summary>
        public static string[] ExtractBulletList(string text)
        {
            List<string> retVal = [];

            var parsed = Markdown.Parse(text);

            foreach (var section in parsed)
            {
                // This would be text describing the bullet list, or maybe text after - just ignore it
                //if (section is ParagraphBlock paragraph)
                //    foreach (var line in paragraph.Inline)
                //        string line_text = line.ToString();

                if (section is ListBlock list)
                {
                    foreach (var list_item in list)
                    {
                        string item_text = ExtractItemText(list_item);

                        // TODO: may need to further filter this text, removing any text inside of parenthesis, or other comments to the side

                        if (item_text != null)
                            retVal.Add(item_text);
                    }
                }
            }

            return retVal.ToArray();
        }

        private static string ExtractItemText(Block list_item)
        {
            var retVal = new StringBuilder();

            if (list_item is ListItemBlock item_block)
            {
                foreach (var sub_block in item_block)       // I'm not sure why this is enumerable
                {
                    if (sub_block is ParagraphBlock para)       // even though it's a paragraph, it should just be a single string (this is a parsed bullet list) -- had an example where it was bold, so extra slices for the bold syntax
                    {
                        foreach (var sub_sub_slice in para.Inline)
                        {
                            if (sub_sub_slice is LiteralInline text_slice)
                            {
                                if (retVal.Length > 0)
                                    retVal.Append(' ');

                                retVal.Append(sub_sub_slice.ToString());
                            }
                        }
                    }
                }
            }

            return retVal.ToString();
        }
    }
}
